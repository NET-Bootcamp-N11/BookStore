using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.useCases.Books.Commands
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Book>
    {
        private readonly IAppDbContext _context;
        public DeleteBookCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Book> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Books.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (user is not null)
            {
                var entry = _context.Books.Remove(user);

                await _context.SaveChangesAsync();

                return entry.Entity;
            }
            return null;
        }
    }
}
