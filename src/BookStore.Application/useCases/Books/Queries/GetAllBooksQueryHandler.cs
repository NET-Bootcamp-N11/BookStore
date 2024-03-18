using BookStore.Application.Abstractions;
using BookStore.Application.Extensions;
using BookStore.Domain.Entities;
using MediatR;

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
            return _appDbContext.Books.AsQueryable().ToPagedList();
        }
    }
}
