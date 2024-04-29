using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BookStore.Application.useCases.Authors.Commands
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Author>
    {
        public readonly IAppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateAuthorCommandHandler(IAppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Author> Handle(CreateAuthorCommand command, CancellationToken cancellation)
        {
            var file = command.Photo;
            string filePath = "";
            string fileName = "";
            try
            {
                var newFilePath = "";
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                filePath = Path.Combine(_webHostEnvironment.WebRootPath, "AuthorsProfileImage", fileName);


                if (!Directory.Exists(newFilePath))
                {
                    for (int i = 0; i < filePath.Split("\\").Length; i++)
                    {
                        if (i + 1 != filePath.Split("\\").Length)
                            newFilePath += filePath.Split("\\")[i] + "\\";
                    }
                 
                    Directory.CreateDirectory(newFilePath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                    await file.CopyToAsync(stream);

            } catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error: {ex.Message}");
            }

            var author = command.Adapt<Author>();
            author.PhotoPath = "/AuthorsProfileImage/" + fileName;
            var res = await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            return res.Entity;
        }
    }
}
