using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.useCases.Authors.Commands
{
    public class CreateAuthorCommandHanler : IRequestHandler<CreateAuthorCommand, string>
    {
        private readonly IAppDbContext _appDbContext;

        public CreateAuthorCommandHanler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<string> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = request.Adapt<Author>();
            await _appDbContext.Authors.AddAsync(author);
            return "Author added";
        }
    }
}
