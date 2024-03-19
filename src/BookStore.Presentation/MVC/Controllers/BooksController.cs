using BookStore.Application.useCases.Authors.Queries;
using BookStore.Application.useCases.Books.Commands;
using BookStore.Application.useCases.Books.Queries;
using BookStore.Application.useCases.Genres.Queries;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly IMediator _mediator;
        private const int pageSize = 10;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var query = new GetAllBooksQuery();
            var allBooks = await _mediator.Send(query);

            var paginatedBooks = PaginateBooks(allBooks, page);

            var viewModel = new BookListViewModel
            {
                Books = paginatedBooks,
                StartIndex = (page - 1) * pageSize,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(allBooks.Count / (double)pageSize),
                PageSize = pageSize
            };

            return View(viewModel);
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
            var authorsquery = new GetAllAuthorsQuery();
            List<Author> authors = await _mediator.Send(authorsquery);
            var book = await _mediator.Send(new GetBookByIdQuery { Id = id });

            var viewModel = new BooksUpdateBookViewModel
            {
                book = book,
                authors = authors
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

        private List<Book> PaginateBooks(List<Book> books, int page)
        {
            int startIndex = (page - 1) * pageSize;
            int count = Math.Min(pageSize, books.Count - startIndex);
            return books.GetRange(startIndex, count);
        }
    }
}
