using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.useCases.Authors.Commands
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Author>
    {

        private readonly IAppDbContext _context;
        public UpdateAuthorCommandHandler(IAppDbContext context)
            => _context = context;

        public async Task<Author> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (author is null)
                throw new Exception("Author not found");

            author.Name = request.Name;
            author.Description = request.Description;

            var entry = _context.Authors.Update(author);
            await _context.SaveChangesAsync();

            return entry.Entity;
        }
    }
}
