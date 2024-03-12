using BookStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.useCases.Books.Commands
{
    public class UpdateAuthorCommand: IRequest<Author>
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
