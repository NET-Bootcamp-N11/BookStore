using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BookStore.Application.useCases.Authors.Commands
{
    public class CreateAuthorCommand : IRequest<Author>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
    }
}
