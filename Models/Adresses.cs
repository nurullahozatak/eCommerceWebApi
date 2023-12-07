namespace ArtTrade.Models
{
    public class Adresses
    {
        public int Id { get; set; }
        public int AddressesId { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Neighborhood  { get; set; }
        public string Street { get; set; }
        public string DoorNo { get; set; }
        public string Floor { get; set; }
    }
}
