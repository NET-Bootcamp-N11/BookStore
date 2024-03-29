using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder
                .Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.Description)
                .HasMaxLength(100)
                .IsRequired();

           /* builder
                .HasData(
                    new List<Author>
                    {
                        new Author{ Id = 1, Name = "O'tkir Hoshimov", Description = "Zo'r inson"},
                        new Author{ Id = 2, Name = "Abdulla Qodiriy", Description = "Zo'r inson"},
                        new Author{ Id = 3, Name = "Xudoyberdi To'xtaboyev", Description = "Zo'r inson"},
                        new Author{ Id = 4, Name = "Asqad Maxtor", Description = "Zo'r inson"},
                        new Author{ Id = 5, Name = "Erkin Vohidov", Description = "Zo'r inson"},
                    }
                );*/
        }
    }
}
