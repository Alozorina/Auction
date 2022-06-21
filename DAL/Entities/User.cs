using DAL.CustomValidationAttributes;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class User : IdentityUser
    {
        [Required, StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required, StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
        [DataType(DataType.Date), BirthDate]
        public DateTime BirthDate { get; set; }


        public virtual ICollection<Item> Purchases { get; set; }
        public virtual ICollection<Item> Lots { get; set; }
    }
}
