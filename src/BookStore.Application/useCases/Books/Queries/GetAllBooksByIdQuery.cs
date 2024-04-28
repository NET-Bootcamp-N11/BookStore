using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Books.Queries
{
    public class GetAllBooksByIdQuery : IRequest<List<Book>>
    {
        public List<long> Ids { get; set; }
    }
}
