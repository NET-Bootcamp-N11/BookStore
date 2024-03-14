using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.useCases.Books.Queries
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IAppDbContext _appDb;

        public GetBookByIdQueryHandler(IAppDbContext appDb)
        {
            _appDb = appDb;
        }

        public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _appDb.Books.FirstOrDefaultAsync(x => x.Id == request.Id);
            return user;
        }
    }
}
