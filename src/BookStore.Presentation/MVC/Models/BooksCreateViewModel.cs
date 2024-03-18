using BookStore.Application.useCases.Books.Commands;
using BookStore.Domain.Entities;

namespace MVC.Models
{
    public class BooksCreateViewModel
    {
        public CreateBookCommand CreateBookCommand { get; set; }
        public List<Author> Authors { get; set; }
        public List<Genre> Genres { get; set; }
    }
}
