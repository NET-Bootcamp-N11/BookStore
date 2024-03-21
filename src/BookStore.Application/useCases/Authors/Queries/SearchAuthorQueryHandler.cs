using BookStore.Application.Abstractions;
using BookStore.Application.useCases.Authors.Queries;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.UseCases.Authors.Queries
{
    public class SearchAuthorQueryHandler : IRequestHandler<SearchAuthorQuery, List<Author>>
    {
        private readonly IAppDbContext _appDbContext;

        public SearchAuthorQueryHandler(IAppDbContext appDbContext)
            => _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));

        public async Task<List<Author>> Handle(SearchAuthorQuery request, CancellationToken cancellationToken)
        {
            if (String.IsNullOrWhiteSpace(request.Text))
                return _appDbContext.Authors.ToList();

            var lowerText = request.Text.ToLower();

            var authors = await _appDbContext.Authors
                .Where(x => x.Name.ToLower().Contains(lowerText)
                    || x.Description.ToLower().Contains(lowerText))
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Description)
                .ToListAsync(cancellationToken);

            if (authors == null)
                throw new Exception("Authors not found");

            return authors;
        }
    }
}
