using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.useCases.Authors.Commands
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Author>
    {
        private readonly IAppDbContext _appDbContext;

        public DeleteAuthorCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        async Task<Author> IRequestHandler<DeleteAuthorCommand, Author>.Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {

            var author = await _appDbContext.Authors.FirstOrDefaultAsync(x => x.Id == request.Id);

            _appDbContext.Authors.Remove(author);

            await _appDbContext.SaveChangesAsync();

            return author;
        }
    }
}
