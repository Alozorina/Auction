using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class UserCreds
    {
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password), StringLength(32, MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
