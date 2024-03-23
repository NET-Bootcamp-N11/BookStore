using BookStore.Application.useCases.Genres.Commands;
using BookStore.Application.useCases.Genres.Queries;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    [Authorize]
    public class GenresController : Controller
    {
        private readonly IMediator _mediator;

        public GenresController(IMediator mediator)
            => _mediator = mediator;

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            GetAllGenreQuery? query = new GetAllGenreQuery();
            var genres = await _mediator.Send(query);

            return View(genres);
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
