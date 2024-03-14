using BookStore.Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.useCases.Authors.Commands
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
    {
        private readonly IAppDbContext _appDbContext;

        public DeleteAuthorCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _appDbContext.Authors.FirstOrDefaultAsync(x => x.Id == request.Id);

            _appDbContext.Authors.Remove(author);

            await _appDbContext.SaveChangesAsync();
        }
    }
}
