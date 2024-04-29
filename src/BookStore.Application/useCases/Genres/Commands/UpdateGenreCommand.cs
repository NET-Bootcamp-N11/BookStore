using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Genres.Commands
{
    public class UpdateGenreCommand : IRequest<Genre>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
