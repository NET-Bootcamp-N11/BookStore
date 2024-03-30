using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.useCases.Authors.Queries
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, List<Author>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetAllAuthorsQueryHandler(IAppDbContext appDbContext)
            => _appDbContext = appDbContext;

        public async Task<List<Author>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            if (request.Text is null)
                return await _appDbContext.Authors.ToListAsync(cancellationToken);
            else
            {
                var lowerText = request.Text.ToLower();

                return await _appDbContext.Authors
                    .Where(x => x.Name.ToLower().Contains(lowerText)
                    || x.Description.ToLower().Contains(lowerText))
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
