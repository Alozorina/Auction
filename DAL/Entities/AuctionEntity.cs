using DAL.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class AuctionEntity : Base
    {
        [Required]
        public string Name { get; set; }
        [Required, StartSaleDate, DataType(DataType.Date)]
        public DateTime StartSaleDate { get; set; }
        [Required, EndSaleDate, DataType(DataType.Date)]
        public DateTime EndSaleDate { get; set; }
        [MaxLength(1500)]
        public string Description { get; set; }
        [Required]
        public int StatusId { get; set; }


        public virtual ICollection<Item> Items { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<AuctionCategory> AuctionCategories { get; set; }
    }
}
