using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Books.Commands
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Book>
    {
        public Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
