using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.useCases.Authors.Queries
{
    public class GetAuthorByNameQueryHandler : IRequestHandler<GetAuthorByNameQuery, Author>
    {
        private readonly IAppDbContext _appDbContext;

        public GetAuthorByNameQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Author> Handle(GetAuthorByNameQuery request, CancellationToken cancellationToken)
        {
            var res = await _appDbContext.Authors.FirstOrDefaultAsync(x => x.Name.Contains(request.Name));
            if (res == null)
            {
                throw new Exception("Author Not found");
            }

            return res;
        }
    }
}
