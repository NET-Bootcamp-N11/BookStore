using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.useCases.Authors.Commands
{
    public class CreateAuthorCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
