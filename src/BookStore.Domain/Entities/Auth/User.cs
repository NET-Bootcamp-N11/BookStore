using Microsoft.AspNetCore.Identity;

namespace BookStore.Domain.Entities.Auth
{
    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public string? PhotoPath { get; set; }
    }
}
