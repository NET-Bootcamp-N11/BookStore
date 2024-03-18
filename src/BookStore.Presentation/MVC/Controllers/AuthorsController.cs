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
        private const int pageSize = 10;

        public AuthorsController(IMediator mediator)
            => _mediator = mediator;

        public async Task<IActionResult> Index(int page = 1)
        {
            var query = new GetAllAuthorsQuery();
            var allAuthors = await _mediator.Send(query);

            page = Math.Max(1, Math.Min(page, TotalPages(allAuthors)));

            var paginatedAuthors = PaginateAuthors(allAuthors, page);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = TotalPages(allAuthors);

            return View(paginatedAuthors);
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

        private List<Author> PaginateAuthors(List<Author> authors, int page)
        {
            int startIndex = (page - 1) * pageSize;
            int count = Math.Min(pageSize, authors.Count - startIndex);
            return authors.GetRange(startIndex, count);
        }

        private int TotalPages(List<Author> authors)
        {
            return (int)Math.Ceiling((double)authors.Count / pageSize);
        }
    }
}
