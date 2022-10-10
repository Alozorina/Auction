using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class ItemUpdateBid
    {
        [Required]
        public decimal CurrentBid { get; set; }
        [Required]
        public int BuyerId { get; set; }
    }
}
