using BookStore.Application.useCases.Books.Commands;
using BookStore.Application.useCases.Books.Queries;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic; 

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

        public async Task<IActionResult> Update(int id)
        {
            var getBookByIdQuery = new GetBookByIdQuery() 
            {
                Id = id
            };
        
            var book = await _mediator.Send(getBookByIdQuery);
        
            if (book == null)
            {
                return NotFound(); 
            }
        
            var updateBookCommand = new UpdateBookCommand()
            {
                Id = id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = book.AuthorId,
            };
        
            var updatedBook = await _mediator.Send(updateBookCommand);
        
            return RedirectToAction(nameof(Index));
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
