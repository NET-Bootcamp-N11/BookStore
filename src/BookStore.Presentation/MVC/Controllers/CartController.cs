using BookStore.Application.useCases.Books.Queries;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
            => _mediator = mediator;

        public async Task<IActionResult> AddToCart(int bookId)
        {
            HttpContext.Request.Cookies.TryGetValue("cart", out string cartItemsJson);
            cartItemsJson ??= "[]";

            var cartItems = JsonSerializer.Deserialize<List<CardItem>>(cartItemsJson);


            if (cartItems.Select(x => x.BookId).Contains(bookId))
            {
                cartItems.FirstOrDefault(x => x.BookId == bookId).Count++;
            }
            else
            {
                cartItems.Add(new CardItem()
                {
                    BookId = bookId,
                    Count = 1
                });
            }

            HttpContext.Response.Cookies
                .Append(
                    "cart",
                    JsonSerializer.Serialize(
                        cartItems,
                        new JsonSerializerOptions()
                        {
                            ReferenceHandler = ReferenceHandler.IgnoreCycles
                        }
                    )
            );

            return RedirectToAction(nameof(Basket));
        }

        public async Task<IActionResult> Basket()
        {
            HttpContext.Request.Cookies.TryGetValue("cart", out string cartItemsJson);
            cartItemsJson ??= "[]";
            var cartItems = JsonSerializer.Deserialize<List<CardItem>>(cartItemsJson);
            var books = await _mediator.Send(new GetAllBooksByIdQuery()
            {
                Ids = cartItems.Select(x => x.BookId).ToList()
            });

            var cart = cartItems.Select(x => new CartItemViewModel()
            {
                Count = x.Count,
                Book = books.FirstOrDefault(book => book.Id == x.BookId)
            }).ToList();

            return View(cart);
        }
    }
}
