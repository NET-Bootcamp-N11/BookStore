using BookStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.useCases.Genres.Queries
{
    public class GetGenreByNameQuery: IRequest<List<Genre>>
    {
        public string Name { get; set; }

    }
}
