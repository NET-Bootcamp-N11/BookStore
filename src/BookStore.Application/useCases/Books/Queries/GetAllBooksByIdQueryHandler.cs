using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.useCases.Books.Queries
{
    public class GetAllBooksByIdQueryHandler : IRequestHandler<GetAllBooksByIdQuery, List<Book>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetAllBooksByIdQueryHandler(IAppDbContext context)
            => _appDbContext = context;

        public async Task<List<Book>> Handle(GetAllBooksByIdQuery request, CancellationToken cancellationToken)
        {
            return await _appDbContext.Books
                   .Where(x => request.Ids.Contains(x.Id))
                .ToListAsync(cancellationToken);
        }
    }
}
