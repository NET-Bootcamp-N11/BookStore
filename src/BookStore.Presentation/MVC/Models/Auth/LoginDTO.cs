using Microsoft.AspNetCore.Authentication;

namespace MVC.Models.Auth
{
    public class LoginDTO
    {
        public string Password { get; set; }
        public string Email { get; set; }

        public string? ReturnUrl { get; set; }
        public IList<AuthenticationScheme>? ExternalLogins { get; set; }
    }
}
