namespace DAL.Entities
{
    public class AuctionCategory : Base
    {
        public int AuctionId { get; set; }
        public AuctionEntity Auction { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
