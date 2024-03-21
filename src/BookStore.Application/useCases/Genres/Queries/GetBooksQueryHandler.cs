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
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<Book>>
    {

        private readonly IAppDbContext _appDbContext;

        public GetBooksQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }
        public async Task<List<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _appDbContext.Books
                .Where(x => (x.Title != null && x.Title.ToLower().StartsWith(request.Title.ToLower())) || (x.Description != null && x.Description.ToLower().Contains(request.Description.ToLower())))
                .ToListAsync(cancellationToken);


            if (books != null)
            {
                return books;
            }

            throw new Exception("Books not found!");
        }
    }
}
