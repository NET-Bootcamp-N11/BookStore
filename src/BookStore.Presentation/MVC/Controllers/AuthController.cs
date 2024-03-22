using BookStore.Application.useCases.Auth;
using BookStore.Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginCommand login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user is null)
                throw new Exception("user not found");

            var passwordCheck = await _userManager.CheckPasswordAsync(user, login.Password);

            if (!passwordCheck)
                throw new Exception("Password yoki Email xato");

            var signInResult = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);

            if (!signInResult.Succeeded)
                throw new Exception("Xato login qilishda");

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterCommand register)
        {
            var user = await _userManager.FindByEmailAsync(register.Email);

            if (user is not null)
                return RedirectToAction(nameof(Login));

            user = new User()
            {
                FullName = register.FullName,
                Email = register.Email,
                UserName = register.UserName,
            };

            var identityResult = await _userManager.CreateAsync(user, register.Password);

            if (!identityResult.Succeeded)
                throw new Exception("Registrationda xato");

            var signInResult = await _signInManager.PasswordSignInAsync(user, register.Password, false, false);

            if (!signInResult.Succeeded)
                throw new Exception("Xato login qilishda");

            return RedirectToAction("Index", "Home");
        }
    }
}
