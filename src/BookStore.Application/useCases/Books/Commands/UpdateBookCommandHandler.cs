using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.useCases.Books.Commands
{
    public class UpdateBookCommandHandler:IRequest<UpdateBookCommand>
    {
        private readonly IAppDbContext _appDbContext;


        public UpdateBookCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Book> Handle(UpdateBookCommand updateBookCommand, CancellationToken cancellationToken)
        {
            var book = await _appDbContext.Books.FirstOrDefaultAsync(x => x.Id == updateBookCommand.Id);
            if (book != null)
            {

                book = updateBookCommand.Adapt<Book>();
                _appDbContext.SaveChangesAsync();
                return book;

            }

            return null;
        }

    }
}
