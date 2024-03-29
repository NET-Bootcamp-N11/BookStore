using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.useCases.Books.Queries
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<Book>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetAllBooksQueryHandler(IAppDbContext context)
        {
            _appDbContext = context;
        }

        public async Task<List<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            if (request.Text is null)
                return await _appDbContext.Books.ToListAsync(cancellationToken);
            else
            {
                var lowerText = request.Text.ToLower();

                return await _appDbContext.Books
                       .Where(x => x.Title.ToLower().Contains(lowerText)
                        || x.Description.ToLower().Contains(lowerText)
                        || x.Author.Name.ToLower().Contains(lowerText))
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
