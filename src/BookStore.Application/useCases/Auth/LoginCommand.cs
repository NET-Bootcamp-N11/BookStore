using MediatR;

namespace BookStore.Application.useCases.Auth
{
    public class LoginCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
