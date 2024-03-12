using BookStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.useCases.Books.Commands
{
    public class DeleteBookCommand : IRequest<Book>
    {
        public long Id { get; set; }
    }
}
