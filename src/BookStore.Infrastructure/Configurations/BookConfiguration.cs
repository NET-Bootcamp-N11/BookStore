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
                        new Book{ Id = 1, AuthorId = 1, Title = "Dunyoning ishlari", Description = "Zo'r kitob"},
                        new Book{ Id = 2, AuthorId = 2, Title = "O'tgan kunlar", Description = "Zo'r kitob"},
                        new Book{ Id = 3, AuthorId = 3, Title = "Besh bolali yigitcha", Description = "Zo'r kitob"},
                        new Book{ Id = 4, AuthorId = 4, Title = "Chinor", Description = "Zo'r kitob"},
                     }
                 );
        }
    }
}
