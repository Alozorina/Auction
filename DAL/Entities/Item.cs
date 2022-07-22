using DAL.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Item : Base
    {
        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [Required, StringLength(80, MinimumLength = 5)]
        public string CreatedBy { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required, DataType(DataType.Currency), Range(double.Epsilon, double.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        public decimal StartingPrice { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal CurrentBid { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required, StartSaleDate, DataType(DataType.Date)]
        public DateTime StartSaleDate { get; set; }
        [Required, EndSaleDate, DataType(DataType.Date)]
        public DateTime EndSaleDate { get; set; }
        [Required]
        public int OwnerId { get; set; }
        public int? BuyerId { get; set; }

        public virtual User Owner { get; set; }
        public virtual User Buyer { get; set; }
        public virtual ICollection<ItemCategory> ItemCategories { get; set; }
        public virtual ICollection<ItemPhoto> ItemPhotos { get; set; }
        public virtual Status Status { get; set; }
    }
}
