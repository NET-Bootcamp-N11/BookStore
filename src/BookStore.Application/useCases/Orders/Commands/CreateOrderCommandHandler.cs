using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Orders.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly IAppDbContext _appDbContext;

        public CreateOrderCommandHandler(IAppDbContext appDbContext)
            => _appDbContext = appDbContext;

        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var entry = await _appDbContext.Orders.AddAsync(request.Order, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return entry.Entity;
        }
    }
}
