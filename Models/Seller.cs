using System.ComponentModel.DataAnnotations.Schema;

namespace ArtTrade.Models
{
    public class Seller
    {
        public int SellerId { get; set; }
        public int Id { get; set; }
        public decimal Income { get; set; }
        public int MyProperty { get; set; }
        public required string Name { get; set; }
        public required string ContactEmail { get; set; }
        [NotMapped]
        public object? Product { get; internal set; }
        public int ProductId { get; set; }
        public required ICollection<Product> Products { get; set; }
    }
}
