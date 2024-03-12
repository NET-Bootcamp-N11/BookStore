using BookStore.Application.Abstractions;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookStore.Infrastructure.Persistance.AppDbContext;

namespace BookStore.Infrastructure.Persistance
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Book> Books { get ; set ; }
        public DbSet<Author> Author { get ; set; }
        public DbSet<Genre> Genres { get ; set; }

        ValueTask<int> IAppDbContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
