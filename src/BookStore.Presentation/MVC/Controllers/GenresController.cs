using BookStore.Application.useCases.Genres.Commands;
using BookStore.Application.useCases.Genres.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class GenresController : Controller
    {
        private readonly IMediator _mediator;

        public GenresController(IMediator mediator)
        {
            _mediator = mediator;
        }

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
    }
}
