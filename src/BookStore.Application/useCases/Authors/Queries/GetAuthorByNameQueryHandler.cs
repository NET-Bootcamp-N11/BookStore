using BookStore.Application.Abstractions;
using BookStore.Application.useCases.Authors.Queries;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.UseCases.Authors.Queries
{
    public class GetAuthorByNameQueryHandler : IRequestHandler<GetAuthorByNameQuery, List<Author>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetAuthorByNameQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }

        public async Task<List<Author>> Handle(GetAuthorByNameQuery request, CancellationToken cancellationToken)
        {
            var authors = await _appDbContext.Authors
                .Where(x => x.Name != null && x.Name.ToLower().StartsWith(request.Name.ToLower()))
                .ToListAsync(cancellationToken);

            if (authors != null)
            {
                return authors;
            }

            throw new Exception("Authors not found");
        
        }
    }
}
