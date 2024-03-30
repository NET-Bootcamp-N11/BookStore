using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BookStore.Application.useCases.Books.Commands
{
    public class CreateBookCommand : IRequest<Book>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
        public decimal Price { get; set; }
        public IFormFile PDFFile { get; set; }
        public int AuthorId { get; set; }
        public int[] Genres { get; set; }
    }
}
