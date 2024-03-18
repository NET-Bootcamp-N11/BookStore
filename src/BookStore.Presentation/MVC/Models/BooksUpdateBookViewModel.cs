using BookStore.Domain.Entities;

namespace MVC.Models
{
    public class BooksUpdateBookViewModel
    {
        public Book book { get; set; }
        public List<Author> authors { get; set; }
    }
}
