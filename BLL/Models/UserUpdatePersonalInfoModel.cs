using DAL.CustomValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class UserUpdatePersonalInfoModel
    {
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password), StringLength(32, MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Date), BirthDate]
        public DateTime? BirthDate { get; set; }
    }
}
