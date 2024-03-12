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
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Book>
    {
        private readonly IAppDbContext _context;
        public DeleteBookCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
    
        public async Task<Book> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Books.FirstOrDefaultAsync(x => x.Id == request.Id);
    
            _context.Books.Remove(user);
    
            await _context.SaveChangesAsync();
    
            return user;
        }
    }
}
