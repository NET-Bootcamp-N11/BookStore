using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.useCases.Genres.Commands
{
    public class DeleteGenreByIdCommandHandler : IRequestHandler<DeleteGenreByIdCommand, Genre>
    {
        private readonly IAppDbContext _appDbContext;

        public DeleteGenreByIdCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Genre> Handle(DeleteGenreByIdCommand request, CancellationToken cancellationToken)
        {
            Genre? res = await _appDbContext.Genres.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (res == null)
            {
                throw new Exception("Genre Not found");
            }
            var entry = _appDbContext.Genres.Remove(res);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return entry.Entity;
        }
    }
}
