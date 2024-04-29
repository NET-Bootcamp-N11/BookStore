using BookStore.Application.useCases.Authors.Commands;
using BookStore.Application.useCases.Authors.Queries;
using BookStore.Application.useCases.Books.Queries;
using BookStore.Application.useCases.Extensions.PaginationExtensions;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    [Authorize]
    public class AuthorsController : Controller
    {
        private readonly IMediator _mediator;
        private const int pageSize = 10;

        public AuthorsController(IMediator mediator)
            => _mediator = mediator;

        [AllowAnonymous]
        public async Task<IActionResult> Index(string text, int page = 1)
        {
            var query = new GetAllAuthorsQuery()
            {
                Text = text,
            };
            var allAuthors = await _mediator.Send(query);

            var paginate = new PaginateObjects<Author>(allAuthors, page, pageSize);
            var paginatedAuthors = paginate.paginatedObjects;

            var viewModel = new PaginationViewModel<Author>(allAuthors, paginatedAuthors, page, pageSize, text);

            return View(viewModel);
        }

        [AllowAnonymous]
        public async Task<IActionResult> MoreInfo(int id)
        {
            var author = await _mediator.Send(new GetAuthorByIdQuery() { Id = id });
            var books = await _mediator.Send(new GetAllBooksQuery() { Text = author.Name });

            var viewModel = new AuthorsMoreInfoViewModel()
            {
                Author = author,
                Books = books,
                Host = HttpContext.Request.Host.ToString(),
            };

            return View("MoreInfo", viewModel);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorCommand createAuthorCommand)
        {
            var author = await _mediator.Send(createAuthorCommand);

            var viewModel = new AuthorsMoreInfoViewModel()
            {
                Author = author,
                Host = HttpContext.Request.Host.ToString(),
            };

            return View("MoreInfo", viewModel);
        }

        public async Task<IActionResult> UpdateAsync(int id)
        {
            var author = await _mediator.Send(new GetAuthorByIdQuery { Id = id });

            return View(author);
        }
         
        [HttpPost]
        public async Task<IActionResult> UpdateAsync(Author author)
        {
            var updateAuthorCommand = new UpdateAuthorCommand()
            {
                Name = author.Name,
                Description = author.Description,
                Id = author.Id
            };

            var updatedAuthor = await _mediator.Send(updateAuthorCommand);

            var detailsModel = new AuthorDetailsViewModel
            {
                Author = updatedAuthor,
                Host = HttpContext.Request.Host.ToString(),
            };

            return View("Details", detailsModel);
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var query = new DeleteAuthorCommand()
            {
                Id = id
            };
            var author = await _mediator.Send(query);
            return RedirectToAction(actionName: nameof(Index));
        }
    }
}