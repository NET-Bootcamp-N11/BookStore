using BookStore.Application.Abstractions;
using BookStore.Application.useCases.Books.Queries;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.UseCases.Books.Queries
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IAppDbContext _appDb;
        private readonly IMemoryCache _memoryCache;

        public GetBookByIdQueryHandler(IAppDbContext appDb, IMemoryCache memoryCache)
        {
            _appDb = appDb;
            _memoryCache = memoryCache;
        }

        public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
           
            if (!_memoryCache.TryGetValue($"Book_{request.Id}", out Book book))
            {
              
                book = await _appDb.Books.FirstOrDefaultAsync(x => x.Id == request.Id);

               
                if (book != null)
                {
                    _memoryCache.Set($"Book_{request.Id}", book);
                }
            }

            return book;
        }
    }
}
