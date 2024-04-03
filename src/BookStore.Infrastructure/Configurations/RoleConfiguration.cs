using BookStore.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(new Role[]
            {
                new Role(){ Id = Guid.Parse("c2597d72-c975-48af-8c1e-a2fb033a22dd"), Name = "User", NormalizedName = "USER"},
                new Role(){ Id = Guid.Parse("0849371e-339c-4e99-9db7-69d2e88ce5e4"), Name = "Admin", NormalizedName = "ADMIN"},
                new Role(){ Id = Guid.Parse("814a9fe9-4f17-4fb0-a10f-0cdda6d837c1"), Name = "SuperAdmin", NormalizedName = "SUPERADMIN"}
            });
        }
    }
}
