using BookStore.Application.useCases.Books.Commands;
using BookStore.Application.useCases.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Diagnostics;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var getAllBooks = new GetAllBooksQuery();
            var books = await _mediator.Send(getAllBooks);
            return View(books);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleteBookId = new DeleteBookCommand()
            {
                Id = id
            };

            await _mediator.Send(deleteBookId);

            return RedirectToAction(actionName: nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
