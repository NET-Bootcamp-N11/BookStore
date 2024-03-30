using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Authors.Queries
{
    public class GetAllAuthorsQuery : IRequest<List<Author>>
    {
        public string? Text { get; set; }
    }
}
