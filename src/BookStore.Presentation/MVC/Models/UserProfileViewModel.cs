using BookStore.Domain.Entities.Auth;

namespace MVC.Models
{
    public class UserProfileViewModel
    {
        public User User { get; set; }
        public IFormFile Photo { get; set; }
    }
}
