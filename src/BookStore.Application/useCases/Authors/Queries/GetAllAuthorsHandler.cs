using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Authors.Queries
{
    public class GetAllAuthorsHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<Author>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetAllAuthorsHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<IEnumerable<Author>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult((IEnumerable<Author>)_appDbContext.Author.ToList());// chetkiy
        }
    }
}
