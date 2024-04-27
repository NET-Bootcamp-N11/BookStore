using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using Mapster;
using MediatR;

namespace BookStore.Application.useCases.Genres.Commands
{
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, Genre>
    {
        private readonly IAppDbContext _appDbContext;
        public CreateGenreCommandHandler(IAppDbContext appDbContext)
            => _appDbContext = appDbContext;

        public async Task<Genre> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = request.Adapt<Genre>();

            var res = await _appDbContext.Genres.AddAsync(genre);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return res.Entity;
        }
    }
}
