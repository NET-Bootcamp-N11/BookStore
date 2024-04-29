using BookStore.Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using MVC.Models;
using MVC.Models.Auth;
using System.Security.Claims;

namespace MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IJSRuntime _jsruntime;
        private readonly IMediator _mediator;

        public AuthController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            IMediator mediator,
            IWebHostEnvironment webHostEnvironment,
            IJSRuntime jsruntime,
            RoleManager<Role> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
            _jsruntime = jsruntime;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(string? ReturnUrl = null)
        {
            var model = new LoginDTO
            {
                ReturnUrl = ReturnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(model);
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

            //await _jsruntime.InvokeVoidAsync("window.setToStorage", "ProfileImage", user.PhotoPath);

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

            identityResult = await _userManager.AddToRoleAsync(user, "User");

            if (!identityResult.Succeeded)
                throw new Exception("There is an issue with adding role");

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

            var viewModel = new UserProfileViewModel()
            {
                User = user
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateProfilePhoto(UserProfileViewModel profileViewModel)
        {
            var currentuser = await _userManager.GetUserAsync(User);

            var file = profileViewModel.Photo;
            string filePath = "";
            string fileName = "";
            try
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                filePath = Path.Combine(_webHostEnvironment.WebRootPath, "UserProfileImage", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error: {ex.Message}");
            }

            currentuser.PhotoPath = "/UserProfileImage/" + fileName;

            var result = await _userManager.UpdateAsync(currentuser);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(currentuser);
                return View("Profile", new UserProfileViewModel()
                {
                    User = currentuser
                });
            }
            else
                throw new Exception("Something went wrong");
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

        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            //This call will generate a URL that directs to the ExternalLoginCallback action method in the Account controller
            //with a route parameter of ReturnUrl set to the value of returnUrl.
            var redirectUrl = Url.Action(action: "ExternalLoginCallback", controller: "Auth", values: new { ReturnUrl = returnUrl });
            // Configure the redirect URL, provider and other properties
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            //This will redirect the user to the external provider's login page
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl, string? remoteError)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            LoginDTO loginViewModel = new LoginDTO
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");

                return View("Login", loginViewModel);
            }

            // Get the login information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");

                return View("Login", loginViewModel);
            }

            // If the user already has a login (i.e., if there is a record in AspNetUserLogins table)
            // then sign-in the user with this external login provider
            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            // If there is no record in AspNetUserLogins table, the user may not have a local account
            else
            {
                // Get the email claim value
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    // Create a new user without password if we do not have a user already
                    var user = await _userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new User
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            FullName = info.Principal.FindFirstValue(ClaimTypes.GivenName) + info.Principal.FindFirstValue(ClaimTypes.Surname),
                        };

                        //This will create a new user into the AspNetUsers table without password
                        await _userManager.CreateAsync(user);
                    }

                    // Add a login (i.e., insert a row for the user in AspNetUserLogins table)
                    await _userManager.AddLoginAsync(user, info);

                    //Then Signin the User
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }

                // If we cannot find the user email we cannot continue
                ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Please contact support on info@dotnettutorials.net";

                return View("Error");
            }
        }
    }
}


