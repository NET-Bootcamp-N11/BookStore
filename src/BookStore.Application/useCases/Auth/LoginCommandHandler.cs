/*using BookStore.Domain.Entities.Auth;
using MediatR;

namespace BookStore.Application.useCases.Auth
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, bool>
    {
        private readonly SignInManager<User> _signInManager;

        public LoginCommandHandler(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
*/