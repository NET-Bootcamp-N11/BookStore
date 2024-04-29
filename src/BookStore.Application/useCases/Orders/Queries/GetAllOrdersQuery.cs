using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Orders.Queries
{
    public class GetAllOrdersQuery : IRequest<List<Order>>
    {
        public Guid? UserId { get; set; }
    }
}
