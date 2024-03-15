using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Genres.Queries
{
    public class GetAllGenreQuery: IRequest<List<Genre>>
    {

    }
}
