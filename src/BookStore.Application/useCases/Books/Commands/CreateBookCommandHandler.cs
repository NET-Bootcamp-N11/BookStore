using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using BookStore.Domain.Exceptions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

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

            if (".pdf" != System.IO.Path.GetExtension(file.FileName))
            {
                throw new ValidationException("Faqat .pdf bo'loladi");
            }

            string filePath = "";
            string fileName = "";

            var PhotoFile = request.Photo;
            string PhotofilePath = "";
            string PhotofileName = "";

            try
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Books", fileName);

                PhotofileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                PhotofilePath = Path.Combine(_webHostEnvironment.WebRootPath, "BookPhoto", PhotofileName);


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                using (var PhotoStream = new FileStream(PhotofilePath, FileMode.Create))
                {
                    await PhotoFile.CopyToAsync(PhotoStream);
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

            var book = request.Adapt<Book>();
            book.Genres = genres;
            book.PDFFilePath = "Books/" + fileName;
            book.PhotoPath = "/BookPhoto/" + PhotofileName;

            var res = await _appDbContext.Books.AddAsync(book);
            await _appDbContext.SaveChangesAsync();
            var storageBook = res.Entity;
            storageBook.Author = await _appDbContext.Authors.FirstOrDefaultAsync(x => x.Id == request.AuthorId);

            return storageBook;

        }
    }
}
