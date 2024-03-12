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
        public Task<string> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
