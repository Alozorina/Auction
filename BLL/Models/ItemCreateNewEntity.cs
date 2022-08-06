using DAL.CustomValidationAttributes;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BLL.Models
{
    public class ItemCreateNewEntity
    {
        [Required, StringLength(80, MinimumLength = 3)]
        public string Name { get; set; }
        [Required, StringLength(80, MinimumLength = 3)]
        public string CreatedBy { get; set; }
        [Required]
        public int OwnerId { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required, DataType(DataType.Currency), Range(double.Epsilon, double.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        public decimal StartingPrice { get; set; }
        [Required, StartSaleDate, DataType(DataType.Date)]
        public DateTime StartSaleDate { get; set; }
        [Required, EndSaleDate, DataType(DataType.Date)]
        public DateTime EndSaleDate { get; set; }
        public ICollection<ItemCategory> ItemCategories { get; set; }
        [Required]
        public virtual List<IFormFile> ItemFormFilePhotos { get; set; }
    }
}
