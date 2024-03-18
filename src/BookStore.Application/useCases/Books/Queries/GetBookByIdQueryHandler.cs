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
            Book? user = await _appDb.Books.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user is null)
                throw new ArgumentException("User not found");

            return user;
        }
    }
}
