using BookStore.Domain.Entities;
using System.Drawing.Printing;

namespace MVC.Models
{
    public class PaginationViewModel<T>
    {
        public List<T> Objects { get; set; }
        public int StartIndex { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public string SearchText { get; set; }

        public PaginationViewModel(List<T> allObjects, List<T> paginatedObjects, int page, int pageSize, string searchText = "")
        {
            Objects = paginatedObjects;
            StartIndex = (page - 1) * pageSize;
            CurrentPage = page;
            TotalPages = (int)Math.Ceiling(allObjects.Count / (double)pageSize);
            PageSize = pageSize;
            SearchText = searchText;
        }
    }
}
