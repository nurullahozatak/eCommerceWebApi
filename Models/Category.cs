namespace ArtTrade.Models
{
    public class Category
    {
        public required int Id { get; set; }
        public required int CategoryId { get; set; }
        public required string Name { get; set; }
        public required List<Seller> Sellers { get; set; }
        public required Product Product { get; set; }
        public required ICollection<Product> Products { get; set; }
    }
}
