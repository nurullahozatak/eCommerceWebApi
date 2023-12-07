namespace ArtTrade.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public int CouponId { get; set; }
        public int CustomerId { get; set; }
        public required string Code { get; set; }
        public decimal DiscountAmount { get; set; }
        // Diğer özellikler

        public required Customer Customer { get; set; }
    }
}
