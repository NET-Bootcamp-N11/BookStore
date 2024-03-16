using BookStore.Application.useCases.Books.Commands;
using BookStore.Application.useCases.Books.Queries;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;

        public BooksController(IMediator mediator, IMemoryCache memoryCache)
        {
            _mediator = mediator;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> Index()
        {
            List<Book> books = _memoryCache.Get("books") as List<Book>;

            if (books is null)
            {
                var getAllBooks = new GetAllBooksQuery();
                books = await _mediator.Send(getAllBooks);

                _memoryCache.Set("books", books);
            }

            return View(books);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleteBookCommand = new DeleteBookCommand()
            {
                Id = id
            };

            var book = await _mediator.Send(deleteBookCommand);

            return RedirectToAction(actionName: nameof(Index));
        }
    }
}
