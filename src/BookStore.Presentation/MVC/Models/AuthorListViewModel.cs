using BookStore.Domain.Entities;

namespace MVC.Models
{
    public class AuthorListViewModel
    {
        public List<Author> Authors { get; set; }
        public int StartIndex { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize = 10;
    }
}
