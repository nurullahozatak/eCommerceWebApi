namespace ArtTrade.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; } // Customer'a referans veren bir özellik
                                            // Diğer özellikler

        // Customer özelliği
        public required Customer Customer { get; set; }
    }
}
