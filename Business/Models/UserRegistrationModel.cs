using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password), StringLength(32, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
