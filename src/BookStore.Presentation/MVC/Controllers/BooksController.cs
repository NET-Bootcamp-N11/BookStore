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

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var getAllBooks = new GetAllBooksQuery();
            List<Book> books = await _mediator.Send(getAllBooks);
        
            // booksi search stringiga qarab filtering 
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString)
                                        || b.Description.Contains(searchString)
                                        || b.Author.Name.Contains(searchString)).ToList();
            }
        
            return View(books);
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
    }
}
