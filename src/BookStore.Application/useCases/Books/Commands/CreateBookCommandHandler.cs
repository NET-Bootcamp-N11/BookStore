using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace BookStore.Application.useCases.Books.Commands
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Book>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CreateBookCommandHandler(IAppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var genres = _appDbContext.Genres.Where(x => request.Genres.Contains(x.Id)).ToList();

            var file = request.PDFFile;
            string filePath = "";
            string fileName = "";

            try
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Books", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error: {ex.Message}");
            }

            var book = request.Adapt<Book>();
            book.Genres = genres;
            book.PDFFilePath = "Books/" + fileName;

            var res = await _appDbContext.Books.AddAsync(book);
            await _appDbContext.SaveChangesAsync();

            return res.Entity;
        }
    }
}
