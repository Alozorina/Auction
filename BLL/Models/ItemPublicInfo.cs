using DAL.CustomValidationAttributes;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class ItemPublicInfo
    {
        [Required]
        public int Id { get; set; }
        [Required, StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [Required, StringLength(80, MinimumLength = 5)]
        public string CreatedBy { get; set; }
        [Required]
        public int OwnerId { get; set; }
        public int? BuyerId { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required, DataType(DataType.Currency), Range(double.Epsilon, double.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        public decimal StartingPrice { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal CurrentBid { get; set; }
        [Required, StartSaleDate, DataType(DataType.Date)]
        public DateTime StartSaleDate { get; set; }
        [Required, EndSaleDate, DataType(DataType.Date)]
        public DateTime EndSaleDate { get; set; }
        public virtual ICollection<ItemCategory> ItemCategories { get; set; }
        public virtual ICollection<ItemPhoto> ItemPhotos { get; set; }
        [Required]
        public virtual Status Status { get; set; }
    }
}
