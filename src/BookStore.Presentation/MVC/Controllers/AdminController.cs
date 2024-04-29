using BookStore.Application.Abstractions;
using BookStore.Domain.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        

        public AdminController(UserManager<User> userManager)
        {
          
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.Where(x => x.Check == false).ToList();
            return View(users);
        }

        public async Task<IActionResult> UpdateUser(Guid Id) 
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if(user == null)
            {
                throw new Exception("User is null");
            }
            user.Check = true;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UpdateAdmin(Guid Id) 
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if(user == null) 
            {
                throw new Exception("User is null");
            }
            user.Check = true;
            await _userManager.RemoveFromRoleAsync(user,"User");
            await _userManager.AddToRoleAsync(user, "Admin");
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }
    }
}
