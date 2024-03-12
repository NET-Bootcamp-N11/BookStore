using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Authors.Commands
{
    public class UpdateAuthorCommand : IRequest<Author>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
