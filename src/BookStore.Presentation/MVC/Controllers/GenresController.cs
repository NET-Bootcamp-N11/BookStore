using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class GenresController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
