using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Books.Commands
{
    public class UpdateBookCommand : IRequest<Book>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public int[] Genres { get; set; }
    }
}
