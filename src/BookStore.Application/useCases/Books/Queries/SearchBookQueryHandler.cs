using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.useCases.Books.Queries
{
    public class SearchBookQueryHandler : IRequestHandler<SearchBookQuery, List<Book>>
    {
        private readonly IAppDbContext _appDbContext;

        public SearchBookQueryHandler(IAppDbContext appDbContext)
            => _appDbContext = appDbContext;

        public async Task<List<Book>> Handle(SearchBookQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Text))
                return await _appDbContext.Books.ToListAsync(cancellationToken);

            var lowerText = request.Text.ToLower();

            var books = await _appDbContext.Books
                .Where(x => x.Title.ToLower().Contains(lowerText)
                    || x.Description.ToLower().Contains(lowerText)
                    || x.Author.Name.ToLower().Contains(lowerText))
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return books;
        }
    }
}
