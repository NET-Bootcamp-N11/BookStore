using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using BookStore.Domain.Exceptions;
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

            if (book == null)
                throw new NotFoundException("Book not found");

            var genres = _appDbContext.Genres.Where(x => updateBookCommand.Genres.Contains(x.Id)).ToList();

            book.Title = updateBookCommand.Title;
            book.Description = updateBookCommand.Description;
            book.AuthorId = updateBookCommand.AuthorId;

            book.Genres.Clear();  // Clear existing genres
            foreach (var genre in genres)
            {
                book.Genres.Add(genre);  // Add selected genres
            }

            var entry = _appDbContext.Books.Update(book);
            await _appDbContext.SaveChangesAsync();

            return entry.Entity;
        }
    }
}
