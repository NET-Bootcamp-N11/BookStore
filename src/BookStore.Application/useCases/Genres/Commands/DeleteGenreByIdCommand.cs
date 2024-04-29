using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Genres.Commands
{
    public class DeleteGenreByIdCommand : IRequest<Genre>
    {
        public int Id { get; set; }
    }
}
