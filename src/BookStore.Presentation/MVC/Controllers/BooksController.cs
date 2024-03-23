using BookStore.Application.useCases.Authors.Queries;
using BookStore.Application.useCases.Books.Commands;
using BookStore.Application.useCases.Books.Queries;
using BookStore.Application.useCases.Genres.Queries;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var getAllBooks = new GetAllBooksQuery();
            List<Book> books = await _mediator.Send(getAllBooks);

            return View(books);
        }

        [AllowAnonymous]
        public async Task<IActionResult> MoreInfo(int id)
        {
            var book = await _mediator.Send(new GetBookByIdQuery() { Id = id });
            return View("MoreInfo", book);
        }

        public async Task<IActionResult> CreateAsync()
        {
            var authors = await _mediator.Send(new GetAllAuthorsQuery());
            var genres = await _mediator.Send(new GetAllGenreQuery());

            return View(new BooksCreateViewModel()
            {
                Authors = authors,
                CheckedBoxes = genres.Select(x => new ViewModelCheckBox() { Id = x.Id, Name = x.Name }).ToList(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(BooksCreateViewModel booksCreateViewModel)
        {
            var createBookCommand = booksCreateViewModel.CreateBookCommand;
            createBookCommand.Genres = booksCreateViewModel.ids;
            var book = await _mediator.Send(createBookCommand);

            return View("Details", book);
        }

        public async Task<IActionResult> UpdateAsync(int id)
        {
            var authors = await _mediator.Send(new GetAllAuthorsQuery());
            var genres = await _mediator.Send(new GetAllGenreQuery());

            var book = await _mediator.Send(new GetBookByIdQuery { Id = id });

            var genreIds = book.Genres.Select(x => x.Id).ToList();

            var viewModel = new BooksUpdateBookViewModel
            {
                book = book,
                authors = authors,
                CheckedBoxes = genres.Select(x => new ViewModelCheckBox() { Id = x.Id, Name = x.Name, IsChecked = genreIds.Contains(x.Id) }).ToList(),

            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(BooksUpdateBookViewModel newBook, int id)
        {
            var updateBookCommand = new UpdateBookCommand()
            {
                Id = id,
                Title = newBook.book.Title,
                Description = newBook.book.Description,
                AuthorId = newBook.book.AuthorId,
                Genres = newBook.ids
            };

            var updatedBook = await _mediator.Send(updateBookCommand);

            return RedirectToAction(nameof(MoreInfo), new { id = id });
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

        [AllowAnonymous]
        public async Task<IActionResult> Search(string text)
        {
            var searchBookCommand = new SearchBookQuery()
            {
                Text = text
            };

            var books = await _mediator.Send(searchBookCommand);

            return View("Index", books);
        }
    }
}
