using BookStore.Application.useCases.Books.Commands;
using BookStore.Application.useCases.Books.Queries;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var getAllBooks = new GetAllBooksQuery();
            List<Book> books = await _mediator.Send(getAllBooks);

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

        public async Task<IActionResult> UpdateAsync(int id)
        {
            var book = await _mediator.Send(new GetBookByIdQuery { Id = id });

            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(Book book)
        {
            UpdateBookCommand? updateBookCommand = new UpdateBookCommand()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = book.AuthorId,
            };

            Book? bookUpdated = await _mediator.Send(updateBookCommand);
            return View("Details", bookUpdated);
        }
    }
}
