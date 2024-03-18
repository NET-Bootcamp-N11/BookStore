using BookStore.Application.useCases.Books.Commands;
using BookStore.Domain.Entities;

namespace MVC.Models
{
    public class BooksCreateViewModel
    {
        public CreateBookCommand CreateBookCommand { get; set; }
        public List<Author> Authors { get; set; }
        public List<ViewModelCheckBox> CheckedBoxes { get; set; }
        public int[] ids { get; set; }
    }
}
