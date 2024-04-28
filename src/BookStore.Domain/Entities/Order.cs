using BookStore.Domain.Entities.Auth;

namespace BookStore.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string CartItems { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public virtual User User { get; set; }
    }
}
