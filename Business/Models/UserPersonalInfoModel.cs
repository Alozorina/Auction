using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class UserPersonalInfoModel
    {
        [Required, StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required, StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public string BirthDate { get; set; }
    }
}
