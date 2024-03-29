using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Books.Queries
{
    public class GetAllBooksQuery : IRequest<List<Book>>
    {
        public string? Text { get; set; }
    }
}
