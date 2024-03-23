using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using BookStore.Domain.Entities.Auth;
using BookStore.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure
{
    public class AppDBContext : IdentityDbContext<User, Role, Guid>, IAppDbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
            => Database.Migrate();

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }

        async ValueTask<int> IAppDbContext.SaveChangesAsync(CancellationToken cancellationToken)
            => await base.SaveChangesAsync(cancellationToken);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());

            modelBuilder.Entity<Genre>()
                .HasData(
                    new List<Genre>
                    {
                        new Genre
                        {
                            Id = 1,
                            Name = "Tarixiy"
                        },
                        new Genre
                        {
                            Id = 2,
                            Name = "Fantastic"
                        }
                    }
                );
        }
    }
}
