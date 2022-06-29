using DAL.CustomValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class UserPersonalInfoModel
    {
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [DataType(DataType.Date), BirthDate]
        public DateTime? BirthDate { get; set; }
    }
}
