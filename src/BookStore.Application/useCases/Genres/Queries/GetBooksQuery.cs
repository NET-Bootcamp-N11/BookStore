using BookStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.useCases.Genres.Queries
{
    public class GetBooksQuery:IRequest<List<Book>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
