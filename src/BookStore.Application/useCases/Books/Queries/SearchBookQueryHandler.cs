using BookStore.Application.Abstractions;
using BookStore.Application.useCases.Authors.Queries;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookStore.Application.useCases.Books.Queries
{
    public class SearchBookQueryHandler : IRequestHandler<SearchBookQuery, List<Book>>
    {
        private readonly IAppDbContext _appDbContext;

        public SearchBookQueryHandler(IAppDbContext appDbContext)
            => _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));

        public async Task<List<Book>> Handle(SearchBookQuery request, CancellationToken cancellationToken)
        {
            if (String.IsNullOrWhiteSpace(request.Text))
                return _appDbContext.Books.ToList();

            var lowerText = request.Text.ToLower();

            var books = await _appDbContext.Books
                .Where(x => x.Title.ToLower().Contains(lowerText)
                    || x.Description.ToLower().Contains(lowerText)
                    || x.Author.Name.ToLower().Contains(lowerText))
                .AsNoTracking()
                .OrderBy(x => x.Title)
                .ThenBy(x => x.Description)
                .ToListAsync(cancellationToken);

            if (books == null)
                throw new Exception("Books not found");

            return books;
        }
    }
}
