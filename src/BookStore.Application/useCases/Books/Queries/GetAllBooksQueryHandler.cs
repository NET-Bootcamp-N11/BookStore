using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.useCases.Books.Queries
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<Book>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetAllBooksQueryHandler(IAppDbContext appDbContext)
            => _appDbContext = appDbContext;

        public async Task<List<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
            =>  _appDbContext.Books.ToList();
    }
}
