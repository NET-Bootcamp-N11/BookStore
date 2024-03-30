using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Entities
{
    public class Book
    {
        public long Id { get; set; }
        public string Title { get; set; }
        [MinLength(20)]
        public string Description { get; set; }
        public string PhotoPath { get; set; }
        public decimal Price { get; set; }
        public string PDFFilePath { get; set; }
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
        public virtual List<Genre> Genres { get; set; }
    }
}
