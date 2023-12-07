namespace ArtTrade.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public required string Text { get; set; }
        // Diğer özellikler

        public required Product Product { get; set; }
    }
}
