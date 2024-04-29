using BookStore.Application.useCases.Books.Queries;
using BookStore.Application.useCases.Orders.Commands;
using BookStore.Application.useCases.Orders.Queries;
using BookStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Security.Claims;
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

            var cartItems = JsonSerializer.Deserialize<List<CartItem>>(cartItemsJson);


            if (cartItems.Select(x => x.BookId).Contains(bookId))
            {
                cartItems.FirstOrDefault(x => x.BookId == bookId).Count++;
            }
            else
            {
                cartItems.Add(new CartItem()
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
            var cartItems = JsonSerializer.Deserialize<List<CartItem>>(cartItemsJson);
            var books = await _mediator.Send(new GetAllBooksByIdQuery()
            {
                Ids = cartItems.Select(x => x.BookId).ToList()
            });

            var cart = cartItems.Select(x => new CartItemViewModel()
            {
                Count = x.Count,
                Book = books.FirstOrDefault(book => book.Id == x.BookId)
            }).ToList();

            var totalPrice = cart.Select(x => x.Count * x.Book.Price).Sum();

            return View(new BasketViewModel()
            {
                Items = cart,
                TotalPrice = totalPrice
            });
        }

        public async Task<IActionResult> ClearAsync()
        {
            HttpContext.Response.Cookies.Delete("cart");

            return RedirectToAction(nameof(Basket));
        }

        public async Task<IActionResult> CreateOrderAsync()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Auth");

            HttpContext.Request.Cookies.TryGetValue("cart", out string cartItemsJson);
            cartItemsJson ??= "[]";
            var cartItems = JsonSerializer.Deserialize<List<CartItem>>(cartItemsJson);
            var books = await _mediator.Send(new GetAllBooksByIdQuery()
            {
                Ids = cartItems.Select(x => x.BookId).ToList()
            });

            var cart = cartItems.Select(x => new CartItemViewModel()
            {
                Count = x.Count,
                Book = books.FirstOrDefault(book => book.Id == x.BookId)
            }).ToList();

            var totalPrice = cart.Select(x => x.Count * x.Book.Price).Sum();

            var order = new Order()
            {
                UserId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                TotalPrice = totalPrice,
                CartItems = JsonSerializer.Serialize(cart, new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                })
            };

            order = await _mediator.Send(new CreateOrderCommand()
            {
                Order = order
            });

            //Basket tozolanadi
            HttpContext.Response.Cookies.Delete("cart");

            return RedirectToAction(nameof(Orders));
        }

        public async Task<IActionResult> Orders()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Auth");

            var orders = await _mediator.Send(new GetAllOrdersQuery()
            {
                UserId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
            });

            return View(orders);
        }
    }
}
