using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.useCases.Orders.Queries
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, List<Order>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetAllOrdersQueryHandler(IAppDbContext appDbContext)
            => _appDbContext = appDbContext;

        public async Task<List<Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId is null)
                return await _appDbContext.Orders.ToListAsync(cancellationToken);
            else
                return await _appDbContext.Orders.Where(x => x.UserId == request.UserId).ToListAsync(cancellationToken);
        }
    }
}
