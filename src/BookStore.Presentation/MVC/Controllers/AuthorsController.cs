using BookStore.Application.useCases.Authors.Commands;
using BookStore.Application.useCases.Authors.Queries;
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

        public AuthorsController(IMediator mediator)
            => _mediator = mediator;

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var query = new GetAllAuthorsQuery();
            var authors = await _mediator.Send(query);

            return View(authors);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorCommand createAuthorCommand)
        {
            var author = await _mediator.Send(createAuthorCommand);

            var viewModel = new AuthorDetailsViewModel()
            {
                Author = author,
                Host = HttpContext.Request.Host.ToString(),
            };

            return View("Details", viewModel);
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

            return View("Details", updatedAuthor);
        }

        [AllowAnonymous]
        public async Task<IActionResult> SearchByName(string name)
        {
            var getAuthorByNameCommand = new SearchAuthorQuery()
            {
                Text = name
            };
            var res = await _mediator.Send(getAuthorByNameCommand);
            return View("Index", res);
        }

        public async Task<IActionResult> DeleteAuthor(int id)
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
