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
            book.Genres = new List<Genre>();
            await _appDbContext.SaveChangesAsync();

            var genres = await _appDbContext.Genres.Where(x => updateBookCommand.Genres.Contains(x.Id)).ToListAsync();

            //var genres = await _appDbContext.Genres.Where(x => updateBookCommand.Genres.Contains(x.Id) && !book.Genres.Select(x => x.Id).Contains(x.Id)).ToListAsync();

            if (book == null)
            {
                throw new Exception("Book not found");
            }

            book.Title = updateBookCommand.Title;
            book.Description = updateBookCommand.Description;
            book.AuthorId = updateBookCommand.AuthorId;
            book.Genres = genres;

            var entry = _appDbContext.Books.Update(book);
            await _appDbContext.SaveChangesAsync();

            return entry.Entity;
        }
    }
}
