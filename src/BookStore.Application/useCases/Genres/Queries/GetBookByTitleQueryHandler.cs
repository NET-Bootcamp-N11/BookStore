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
    public class GetBookByTitleQueryHandler : IRequestHandler<GetBookByTitleQuery, List<Book>>
    {

        private readonly IAppDbContext _appDbContext;

        public GetBookByTitleQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }
        public async Task<List<Book>> Handle(GetBookByTitleQuery request, CancellationToken cancellationToken)
        {
            var books = await _appDbContext.Books
                .Where(x => x.Title != null && x.Title.ToLower().StartsWith(request.Title.ToLower()))
                .ToListAsync(cancellationToken);

            if (books != null)
            {
                return books;
            }

            throw new Exception("Books not found!");
        }
    }
}
