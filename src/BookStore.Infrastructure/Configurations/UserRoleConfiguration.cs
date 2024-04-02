using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
        {
            builder.HasData(new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("814a9fe9-4f17-4fb0-a10f-0cdda6d837c1"),
                UserId = Guid.Parse("ca9d3855-2c7d-427a-9364-d37dac608b55")
            });
        }
    }
}
