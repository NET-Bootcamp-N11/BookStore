using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Genres.Commands
{
    public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, Genre>
    {
        private readonly IAppDbContext _appDbContext;

        public UpdateGenreCommandHandler(IAppDbContext appDbContext)
            => _appDbContext = appDbContext;

        public async Task<Genre> Handle(UpdateGenreCommand updateGenreCommand, CancellationToken cancellationToken)
        {
            var genre = _appDbContext.Genres.FirstOrDefault(x => x.Id == updateGenreCommand.Id);

            if (genre == null)
            {
                throw new Exception("Genre not found");
            }

            genre.Id = updateGenreCommand.Id;
            genre.Name = updateGenreCommand.Name;

            var entry = _appDbContext.Genres.Update(genre);
            await _appDbContext.SaveChangesAsync();

            return entry.Entity;
        }
    }
}