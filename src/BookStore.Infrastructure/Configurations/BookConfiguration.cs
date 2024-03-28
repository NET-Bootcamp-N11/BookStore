using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .Property(x => x.Title)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.Description)
                .HasMaxLength(280)
                .IsRequired();

            builder
                 .HasData(
                     new List<Book>
                     {
                        new Book{ Id = 1, AuthorId = 5, Title = "Mening Yulduzlarim", Description = "Zo'r kitob", PDFFilePath = "Books/Erkin Vohidov Mening Yulduzim.pdf", Price = 20000},
                     }
                 );
        }
    }
}
