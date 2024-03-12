using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using Mapster;
using MediatR;

namespace BookStore.Application.useCases.Books.Commands
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Book>
    {
        private readonly IAppDbContext _appDbContext;
        public CreateBookCommandHandler(IAppDbContext appDbContext)
            => _appDbContext = appDbContext;

        public async Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = request.Adapt<Book>();
            var res = await _appDbContext.Books.AddAsync(book);
            await _appDbContext.SaveChangesAsync();
            return res.Entity;
        }
    }
}
