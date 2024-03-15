using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure
{
    public class AppDBContext : DbContext, IAppDbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }

        async ValueTask<int> IAppDbContext.SaveChangesAsync(CancellationToken cancellationToken)
            => await base.SaveChangesAsync(cancellationToken);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStore.MVC.MediatR.DB;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genre>()
                .HasData(
                    new List<Genre>()
                    {
                        new Genre(){
                            Id = 1,
                            Name = "Tarixiy"
                        },
                        new Genre(){
                            Id = 2,
                            Name = "Badiiy"
                        },
                        new Genre(){
                            Id = 3,
                            Name = "Ilmiy"
                        }
                    }
                );

            modelBuilder.Entity<Author>()
               .HasData(new List<Author>()
               {
                   new Author(){ Id = 1, Name = "O'tkir Hoshimov", Description = "Zo'r yozuvchi"},
                   new Author(){ Id = 2, Name = "Toxir Malik", Description = "Zo'r yozuvchi2"},
               });

            modelBuilder.Entity<Book>()
                .HasData(new List<Book>()
                {
                    new Book(){ Id = 1, AuthorId = 1, Title = "Dunyoning ishlari", Description = "Zo'r kitob"},
                    new Book(){ Id = 2, AuthorId = 2, Title = "Shaytanat", Description = "Zo'r kitob2"},
                    new Book(){ Id = 3, AuthorId = 1, Title = "Daftar xoshiyasidagi bitiklar", Description = "Zo'r kitob"},
                });
        }
    }
}
