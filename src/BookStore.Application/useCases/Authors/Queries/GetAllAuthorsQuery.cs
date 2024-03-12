using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Authors.Queries
{
    public class GetAllAuthorsQuery : IRequest<List<Author>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
