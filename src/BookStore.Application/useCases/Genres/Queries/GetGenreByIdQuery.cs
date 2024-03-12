using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Genres.Queries
{
    public class GetGenreByIdQuery : IRequest<Genre>
    {
        public long Id { get; set; }
    }
}

