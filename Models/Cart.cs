namespace ArtTrade.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public required Product Product { get; set; }
        public required string ProductName { get; set; }
    }
}
