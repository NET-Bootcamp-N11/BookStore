using BookStore.Application.useCases.Authors.Commands;
using BookStore.Application.useCases.Authors.Queries;
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
        public async Task<IActionResult> GetAuthors()
        {
            var getAllAuthorsQuery = new GetAllAuthorsQuery();
            List<Author> authors = await _mediator.Send(getAllAuthorsQuery);

            ViewData["Authors"] = authors;

            return View();
        }

        public async Task<IActionResult> UpdateAsync(int id)
        {
            var author = await _mediator.Send(new GetBookByIdQuery { Id = id });

            return View(author);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAsync(Book newBook)
        {
            var updateBookCommand = new UpdateBookCommand()
            {
                Id = newBook.Id,
                Title = newBook.Title,
                Description = newBook.Description,
                AuthorId = newBook.AuthorId,
            };

            var updatedBook = await _mediator.Send(updateBookCommand);

            return View("Details", updatedBook);
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
