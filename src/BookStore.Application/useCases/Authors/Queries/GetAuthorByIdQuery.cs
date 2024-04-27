using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Authors.Queries
{
    public class GetAuthorByIdQuery : IRequest<Author>
    {
        public int Id { get; set; }
    }
}
