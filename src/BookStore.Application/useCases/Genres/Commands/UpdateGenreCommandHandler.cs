using BookStore.Application.Abstractions;
using BookStore.Application.useCases.Authors.Commands;
using BookStore.Application.useCases.Books.Commands;
using BookStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.useCases.Genres.Commands
{
    public class UpdateGenreCommandHandler: IRequestHandler<UpdateGenreCommand, Genre>
    {
        private readonly IAppDbContext _appDbContext;

        public UpdateGenreCommandHandler(IAppDbContext appDbContext)
            => _appDbContext = appDbContext;

        public async Task<Genre> Handle(UpdateGenreCommand updateGenreCommand, CancellationToken cancellationToken)
        {
            var genre =  _appDbContext.Genres.FirstOrDefault(x => x.Id == updateGenreCommand.Id);

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