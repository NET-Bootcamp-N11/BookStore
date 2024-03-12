using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.useCases.Genres.Commands
{
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, Genre>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public CreateGenreCommandHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Genre> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = _mapper.Map<Genre>(request);

            var res = await _appDbContext.Genres.AddAsync(genre);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return res.Entity;
        }
    }
}
