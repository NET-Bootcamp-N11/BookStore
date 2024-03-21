using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.useCases.Authors.Queries
{
    public class SearchAuthorQuery : IRequest<List<Author>>
    {
        public string Text { get; set; }
    }
}
