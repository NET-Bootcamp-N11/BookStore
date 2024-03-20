//using BookStore.Domain.Entities.Auth;
//using MediatR;
//using Microsoft.AspNetCore.Identity;

//namespace BookStore.Application.useCases.Auth
//{
//    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
//    {
//        private readonly UserManager<User> _userManager;

//        public RegisterCommandHandler(UserManager<User> userManager)
//            => _userManager = userManager;

//        public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
//        {
//            var user = new User()
//            {
//                FullName = request.FullName,
//                UserName = request.UserName,
//                Email = request.Email,
//                PasswordHash = request.Password
//            };

//            var result = _userManager.CreateAsync(user);

//            if (!result.IsCompletedSuccessfully)
//                throw new Exception("Error registratsiyada");

//            return true;
//        }
//    }
//}
