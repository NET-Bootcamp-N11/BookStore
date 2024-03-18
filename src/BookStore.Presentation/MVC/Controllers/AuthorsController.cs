using BookStore.Application.useCases.Authors.Commands;
using BookStore.Application.useCases.Authors.Queries;
using BookStore.Application.useCases.Genres.Commands;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
            => _mediator = mediator;

        public async Task<IActionResult> Index(string searchString) 
        {
            var query = new GetAllAuthorsQuery();
            var authors = await _mediator.Send(query);

            if (!string.IsNullOrEmpty(searchString))
            {
                authors = authors.Where(a => a.Name.Contains(searchString)
                                        || a.Description.Contains(searchString)).ToList();
            }

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

            return View("Details", author);
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
