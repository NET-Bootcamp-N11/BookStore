using BookStore.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(new User()
            {
                Id = Guid.Parse("ca9d3855-2c7d-427a-9364-d37dac608b55"),
                FullName = "Admin aka",
                UserName = "adminaka",
                NormalizedUserName = "ADMINAKA",
                Email = "adminaka0618@gmail.com",
                NormalizedEmail = "ADMINAKA0618@GMAIL.COM",
                PasswordHash = "AQAAAAIAAYagAAAAEL5dGfxjT0/cQfyvMMMQ+b+ancTXrKrIV/xliyQbGTpwoIO2zJNi/DYKFHKMs05POg==",
                SecurityStamp = "EQL6PMQHTWTUEC7XXDY6ZS5M3YS6UAZJ",
                ConcurrencyStamp = "fa95c310-1ae7-454d-a589-af70ed8c0bce",
                PhoneNumber = "123456789"
            });
        }
    }
}
