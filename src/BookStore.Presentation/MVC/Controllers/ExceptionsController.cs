using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class ExceptionsController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Error(string message, int code)
        {
            var viewModel = new ExceptionsErrorViewModel
            {
                Message = message,
                Code = code
            };

            return View(viewModel);
        }
    }
}
