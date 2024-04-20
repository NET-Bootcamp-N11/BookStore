using BookStore.Domain.Entities;

namespace MVC.Models
{
    public class AuthorsMoreInfoViewModel
    {
        public Author Author { get; set; }
        public string Host { get; set; }
        public List<Book> Books { get; set; }
    }
}
