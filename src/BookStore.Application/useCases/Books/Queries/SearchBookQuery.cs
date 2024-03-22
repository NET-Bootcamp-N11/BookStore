using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Books.Queries
{
    public class SearchBookQuery : IRequest<List<Book>>
    {
        public string Text { get; set; }
    }
}
