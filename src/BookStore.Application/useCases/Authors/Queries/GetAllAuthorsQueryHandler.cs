using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Authors.Queries
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, List<Author>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetAllAuthorsQueryHandler(IAppDbContext appDbContext)
            => _appDbContext = appDbContext;

        public async Task<List<Author>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            return _appDbContext.Authors.ToList();// chetkiy
        }
    }
}
