using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Books.Commands
{
    public class CreateBookCommand : IRequest<Book>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
    }
}
