using BookStore.Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.Auth;

namespace MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(
            SignInManager<User> signInManager,
            UserManager<User> userManager)
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

        public async Task<IActionResult> Register()
        {
            return View();
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

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var user = await _userManager.FindByEmailAsync(registerDTO.Email);

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

            return RedirectToAction("Index", "Home");
        }
    }
}





// Pull Request jo'natomadim shunchun shetga yozib qo'yyapman - copy/paste


// User controller:



// using Microsoft.AspNetCore.Mvc;
// using MVC.Models;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Identity;
// using BookStore.Domain.Entities.Auth;

// namespace MVC.Controllers
// {
//     public class ProfileController : Controller
//     {
//         private readonly UserManager<User> _userManager;

//         public ProfileController(UserManager<User> userManager)
//         {
//             _userManager = userManager;
//         }

//         public async Task<IActionResult> Index()
//         {
//             var user = await _userManager.GetUserAsync(User);

//             if (user == null)
//             {
//                 // If not authenticated, redirect to login
//                 return RedirectToAction("Login", "Auth");
//             }
            
//             return View(user);
//         }
//     }
// }




// Views (Profile):


// @{
//     ViewData["Title"] = "Profile";
// }

// <h1>Welcome to your Profile</h1>

//     <p>Here you can manage your profile details:</p>

//     <!--  profile details -->
//     <div>
//     <h2>User Information</h2>
//     <p><strong>Full Name:</strong> @User.Identity.Name</p>
//     <p><strong>Email:</strong> @Model.Email</p>
//     <p><strong>Phone Number:</strong> @Model.PhoneNumber</p>
//     </div>

//     <!-- Form to update profile -->
//     <form asp-controller="Profile" asp-action="Update" method="post">
//     <div class="mb-3">
//     <label asp-for="FullName" class="form-label">Full Name</label>
//     <input asp-for="FullName" type="text" class="form-control" required>
//     </div>
//     <div class="mb-3">
//     <label asp-for="Email" class="form-label">Email</label>
//     <input asp-for="Email" type="email" class="form-control" readonly>
// </div>
//     <div class="mb-3">
//     <label asp-for="PhoneNumber" class="form-label">Phone Number</label>
//     <input asp-for="PhoneNumber" type="text" class="form-control">
//     </div>

//     <button type="submit" class="btn btn-primary">Update Profile</button>
//     </form>
