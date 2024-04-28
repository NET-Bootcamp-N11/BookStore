using BookStore.Domain.Entities;

namespace MVC.Models
{
    public class BasketViewModel
    {
        public List<CartItemViewModel> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
