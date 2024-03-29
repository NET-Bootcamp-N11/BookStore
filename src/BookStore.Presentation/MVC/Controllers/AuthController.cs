using BookStore.Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Auth;

namespace MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMediator _mediator;

        public AuthController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IMediator mediator)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
                throw new Exception("User not found");

            var result = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);

            if (!result.Succeeded)
                throw new Exception("There is an issue with signing in process");

            return RedirectToAction("Index", "Books");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var user = await _userManager.FindByEmailAsync(registerDTO.Email);

            if (registerDTO.Password != registerDTO.ConfirmPassword)
                throw new Exception("Passwords do not match!");

            if (user is not null)
                throw new Exception("You are already registred");

            user = new User()
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                FullName = registerDTO.FullName,
            };

            var identityResult = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!identityResult.Succeeded)
                throw new Exception("There is an issue with signing in process");

            var result =
                await _signInManager.PasswordSignInAsync(user, registerDTO.Password, false, false);

            if (!result.Succeeded)
                throw new Exception("There is an issue with signing in process");

            return RedirectToAction("Index", "Books");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);

            return View(user);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(User user)
        {
            var currentuser = await _userManager.GetUserAsync(User);

            var result = await _userManager.SetEmailAsync(currentuser, user.Email);
            if (!result.Succeeded) throw new Exception("Something went wrong");

            result = await _userManager.SetPhoneNumberAsync(currentuser, user.PhoneNumber);
            if (!result.Succeeded) throw new Exception("Something went wrong");

            result = await _userManager.SetUserNameAsync(currentuser, user.UserName);
            if (!result.Succeeded) throw new Exception("Something went wrong");

            currentuser.FullName = user.FullName;

            result = await _userManager.UpdateAsync(currentuser);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(currentuser);
                return View("Profile", currentuser);
            }
            else
                throw new Exception("Something went wrong");
        }
    }
}


