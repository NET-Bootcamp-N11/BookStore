using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Genres.Commands
{
    public class CreateGenreCommand : IRequest<Genre>
    {
        public string Name { get; set; }
    }
}
