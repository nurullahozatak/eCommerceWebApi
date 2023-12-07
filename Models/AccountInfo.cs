namespace ArtTrade.Models
{
    public class AccountInfo
    {
        public int Id { get; set; }
        public int AccountInfoId { get; set; }
        public int CustomerId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; set; }
        public required string Mail { get; set; }
        public required string Phonenumber { get; set; }
        public required Customer Customer { get; set; }
    }

}

