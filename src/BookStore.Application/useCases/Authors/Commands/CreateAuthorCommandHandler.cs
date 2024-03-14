using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using Mapster;
using MediatR;

namespace BookStore.Application.useCases.Authors.Commands
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Author>
    {
        public IAppDbContext _context;

        public CreateAuthorCommandHandler(IAppDbContext context)
            => _context = context;

        public async Task<Author> Handle(CreateAuthorCommand command, CancellationToken cancellation)
        {
            var author = command.Adapt<Author>();
            var res = await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            return res.Entity;
        }
    }
}
