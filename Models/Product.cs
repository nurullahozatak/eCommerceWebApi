using System.ComponentModel.DataAnnotations.Schema;

namespace ArtTrade.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; } // Category'ye referans veren bir özellik
        public int SellerId { get; set; }
        // Diğer özellikler

        // Category özelliği
        public required Category Category { get; set; }

        public required Seller Seller { get; set; }
        public required List<Cart> Carts { get; set; }
        [NotMapped]
        public object? Comment { get; internal set; }
        public required ICollection<Comment> Comments { get; set; }
        [NotMapped]
        public object? Sellers { get; internal set; }
    }
}
