using BookStore.Domain.Entities;

namespace MVC.Models
{
    public class BookSearchViewModel
    {
        public List<Author> Authors { get; set; }
        public List<Author> SearchedAuthors { get; set; }
    }
}
