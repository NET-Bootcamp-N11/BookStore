using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Authors.Commands
{
    public class DeleteAuthorCommand : IRequest<Author>
    {
        public int Id { get; set; }
    }
}
