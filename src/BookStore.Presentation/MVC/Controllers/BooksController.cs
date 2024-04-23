using BookStore.Application.useCases.Authors.Queries;
using BookStore.Application.useCases.Books.Commands;
using BookStore.Application.useCases.Books.Queries;
using BookStore.Application.useCases.Extensions.PaginationExtensions;
using BookStore.Application.useCases.Genres.Queries;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Filters;
using MVC.Models;

namespace MVC.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly IMediator _mediator;
        private const int pageSize = 10;
        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string text, int page = 1)
        {
            var query = new GetAllBooksQuery()
            {
                Text = text
            };
            var allBooks = await _mediator.Send(query);

            var paginate = new PaginateObjects<Book>(allBooks, page, pageSize);
            var paginatedBooks = paginate.paginatedObjects;

            var viewModel = new PaginationViewModel<Book>(allBooks, paginatedBooks, page, pageSize, text);

            return View(viewModel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> MoreInfo(int id)
        {
            var book = await _mediator.Send(new GetBookByIdQuery() { Id = id });

            var viewModel = new BooksMoreInfoViewModel()
            {
                Book = book,
                Host = HttpContext.Request.Host.ToString(),
            };

            return View("MoreInfo", viewModel);
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

            //HttpContext.Request.Protocol

            var viewModel = new BooksMoreInfoViewModel()
            {
                Book = book,
                Host = HttpContext.Request.Host.ToString(),
            };

            return View("MoreInfo", viewModel);
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

        [Delete]
        [Permission]
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
