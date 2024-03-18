using BookStore.Domain.Entities;

namespace MVC.Models
{
    public class BooksUpdateBookViewModel
    {
        public Book book { get; set; }
        public List<Author> authors { get; set; }
        public List<ViewModelCheckBox> CheckedBoxes { get; set; }
        public int[] ids { get; set; }
    }
}
