using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Item : Base
    {
        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required, DataType(DataType.Currency), Range(double.Epsilon, double.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        public decimal StartingPrice { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal CurrentBid { get; set; }
        [Required]
        public int AuctionId { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public int OwnerId { get; set; }
        public int? BuyerId { get; set; }
        public int PhotosId { get; set; }

        [Required]
        public virtual User Owner { get; set; }
        public virtual User Buyer { get; set; }
        public virtual ICollection<ItemCategory> ItemCategories { get; set; }
        public virtual ItemPhoto ItemPhoto { get; set; }
        [Required]
        public virtual AuctionEntity Auction { get; set; }
        public virtual Status Status { get; set; }
    }
}
