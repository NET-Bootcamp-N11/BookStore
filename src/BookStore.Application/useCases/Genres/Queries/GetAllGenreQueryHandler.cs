﻿using BookStore.Application.Abstractions;
using BookStore.Application.Extensions;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Genres.Queries
{
    public class GetAllGenreQueryHandler : IRequestHandler<GetAllGenreQuery, List<Genre>>
    {

        private readonly IAppDbContext _appDbContext;

        public GetAllGenreQueryHandler(IAppDbContext context)
        {
            _appDbContext = context;
        }

        public async Task<List<Genre>> Handle(GetAllGenreQuery request, CancellationToken cancellationToken)
        {
            return _appDbContext.Genres.AsQueryable().ToPagedList();
        }
    }
}

