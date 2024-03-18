using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.useCases.Books.Commands
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Book>
    {
        private readonly IAppDbContext _appDbContext;

        public UpdateBookCommandHandler(IAppDbContext appDbContext)
            => _appDbContext = appDbContext;

        public async Task<Book> Handle(UpdateBookCommand updateBookCommand, CancellationToken cancellationToken)
        {
            var book = await _appDbContext.Books.FirstOrDefaultAsync(x => x.Id == updateBookCommand.Id);

            if (book is null)
                throw new ArgumentException("Book not found");

            book.Title = updateBookCommand.Title;
            book.Description = updateBookCommand.Description;
            book.AuthorId = updateBookCommand.AuthorId;

            var entry = _appDbContext.Books.Update(book);
            await _appDbContext.SaveChangesAsync();
            return entry.Entity;
        }
    }
}
