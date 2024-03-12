using BookStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.useCases.Books.Queries
{
    public class GetAllBooksQuery:IRequest<List<Book>>
    {
    }
}
