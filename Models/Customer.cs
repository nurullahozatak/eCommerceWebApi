using System.ComponentModel.DataAnnotations.Schema;

namespace ArtTrade.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int AccountInfoId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        // Orders özelliği
        public required List<Order> Orders { get; set; }
        [NotMapped]
        public object? Coupon { get;  set; }
        public required ICollection<Coupon> Coupons { get; set; }

        public required AccountInfo AccountInfo { get; set; }
    }
}
