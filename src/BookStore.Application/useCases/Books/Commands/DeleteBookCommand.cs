using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Books.Commands
{
    public class DeleteBookCommand : IRequest<Book>
    {
        public long Id { get; set; }
    }
}
