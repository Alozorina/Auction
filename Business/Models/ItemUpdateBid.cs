using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class ItemUpdateBid
    {
        [Required]
        public decimal CurrentBid { get; set; }
        [Required]
        public int BuyerId { get; set; }
    }
}
