using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using Mapster;
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

            if (book != null)
            {
                book = updateBookCommand.Adapt<Book>();
                var entry = _appDbContext.Books.Update(book);
                await _appDbContext.SaveChangesAsync();

                return entry.Entity;
            }

            return null;
        }
    }
}
