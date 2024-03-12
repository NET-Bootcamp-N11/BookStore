using BookStore.Domain.Entities;

namespace BookStore.Application.useCases.Books.Queries
{
    public class GetBookByIdQuery
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Author Author { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
