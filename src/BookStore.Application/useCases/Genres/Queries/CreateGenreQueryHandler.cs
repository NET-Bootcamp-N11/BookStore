using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.useCases.Genres.Queries
{
    public class CreateGenreQueryHandler : IRequestHandler<CreateGenreQuery, Genre>
    {
        private readonly IAppDbContext _appDbContext;

        public CreateGenreQueryHandler(IAppDbContext dbContext)
        {
            _appDbContext = dbContext;
        }

        public async Task<Genre> Handle(CreateGenreQuery request, CancellationToken cancellationToken)
        {
            var newGenre = new Genre
            {
                
                Name = request.Name
            };
            if (_appDbContext.Genres.Any(x => x.Name != newGenre.Name))
            {
                var entityEntry = await _appDbContext.Genres.AddAsync(newGenre, cancellationToken);

                await _appDbContext.SaveChangesAsync(cancellationToken);

                return newGenre;
            }

            return new Genre();

        }
    }
}
