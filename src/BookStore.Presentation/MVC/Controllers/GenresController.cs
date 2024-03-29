using BookStore.Application.useCases.Books.Queries;
using BookStore.Application.useCases.Extensions.PaginationExtensions;
using BookStore.Application.useCases.Genres.Commands;
using BookStore.Application.useCases.Genres.Queries;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    [Authorize]
    public class GenresController : Controller
    {
        private readonly IMediator _mediator;
        private const int pageSize = 10;

        public GenresController(IMediator mediator)
            => _mediator = mediator;

        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
        {
            var query = new GetAllGenreQuery();
            var allGenres = await _mediator.Send(query);

            var paginate = new PaginateObjects<Genre>(allGenres, page, pageSize);
            var paginatedGenres = paginate.paginatedObjects;

            var viewModel = new PaginationViewModel<Genre>(allGenres, paginatedGenres, page, pageSize);

            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGenreCommand createGenreCommand)
        {
            var genre = await _mediator.Send(createGenreCommand);

            return View("Details", genre);
        }

        public async Task<IActionResult> DeleteGenre(int id)
        {
            var query = new DeleteGenreByIdCommand()
            {
                Id = id
            };
            var res = await _mediator.Send(query);

            return RedirectToAction(actionName: nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var author = await _mediator.Send(new GetGenreByIdQuery() { Id = id });

            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Genre genre)
        {
            var updateGenreCommand = new UpdateGenreCommand()
            {
                Id = genre.Id,
                Name = genre.Name,
            };

            var updatedGenre = await _mediator.Send(updateGenreCommand);

            return View("Details", updatedGenre);
        }
    }
}
