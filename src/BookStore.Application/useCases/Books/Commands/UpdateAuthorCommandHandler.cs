using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.useCases.Books.Commands
{
    public class UpdateAuthorCommandHandler: IRequestHandler<UpdateAuthorCommand, Author>
    {

        private readonly IAppDbContext _context;
        public UpdateAuthorCommandHandler(IAppDbContext context)
            => _context = context;

        public async Task<Author> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _context.Author.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (author is not null)
            {
                author.Name = request.Name;
                author.Description = request.Description;

                await _context.SaveChangesAsync();
            }

            return null;
        }

    }
}
