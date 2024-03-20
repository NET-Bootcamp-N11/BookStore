using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.useCases.Genres.Queries
{
    public class GetGenreByNameQueryHandler : IRequestHandler<GetGenreByNameQuery, List<Genre>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetGenreByNameQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }

        public async Task<List<Genre>> Handle(GetGenreByNameQuery request, CancellationToken cancellationToken)
        {
            var genres = await _appDbContext.Genres
                .Where(x => x.Name != null && x.Name.ToLower().StartsWith(request.Name.ToLower()))
                .ToListAsync(cancellationToken);

            if(genres != null)
            {
                return genres;
            }

            throw new Exception("Genres not found!");
        }
    }
}
