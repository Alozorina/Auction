using DAL.CustomValidationAttributes;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class UserPulicInfo
    {
        [Required, StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required, StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Date), BirthDate]
        public DateTime? BirthDate { get; set; }
        public Role Role { get; set; }
        public virtual ICollection<Item> Purchases { get; set; }
        public virtual ICollection<Item> Lots { get; set; }
    }
}
