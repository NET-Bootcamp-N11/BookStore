using BookStore.Application.Abstractions;
using BookStore.Application.useCases.Books.Queries;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BookStore.Application.UseCases.Books.Queries
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<Book>>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMemoryCache _memoryCache;

        public GetAllBooksQueryHandler(IAppDbContext context, IMemoryCache memoryCache)
        {
            _appDbContext = context;
            _memoryCache = memoryCache;
        }

        public async Task<List<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {

            if (!_memoryCache.TryGetValue("Books", out List<Book> books))
            {
                books = await _appDbContext.Books.ToListAsync(cancellationToken);
                _memoryCache.Set("Books", books);
            }

            return books;
        }
    }
}
