using DAL.CustomValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class User : Person
    {
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public int RoleId { get; set; }
        [DataType(DataType.Date), BirthDate]
        public DateTime? BirthDate { get; set; }

        public Role Role { get; set; }
        public virtual ICollection<Item> Purchases { get; set; }
        public virtual ICollection<Item> Lots { get; set; }
    }
}
