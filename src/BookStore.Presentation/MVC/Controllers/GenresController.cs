using BookStore.Application.useCases.Genres.Queries;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using BookStore.Application.useCases.Genres.Commands;
using BookStore.Domain.Entities;

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

        public async Task<IActionResult> DeleteGenre(int id)
        {
            var query = new DeleteGenreByIdCommand()
            {
                Id = id
            };
            var res = await _mediator.Send(query);
            return RedirectToAction(actionName:nameof(Index));
        }

        public async Task<IActionResult> CreateGenre(Genre newGenre)
        {
            var query = new CreateGenreCommand() { Name = newGenre.Name };
            var res = await _mediator.Send(query);
            return View(res);
        }
    }
}
