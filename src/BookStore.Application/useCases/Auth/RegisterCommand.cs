using MediatR;

namespace BookStore.Application.useCases.Auth
{
    public class RegisterCommand : IRequest<bool>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
