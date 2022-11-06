using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class UserLoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password), StringLength(32, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
