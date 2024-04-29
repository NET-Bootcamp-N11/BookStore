using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Orders.Commands
{
    public class CreateOrderCommand : IRequest<Order>
    {
        public Order Order { get; set; }
    }
}
