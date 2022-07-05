using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class UserCreds
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password), StringLength(32, MinimumLength = 6)]
        public string OldPassword { get; set; }
        [DataType(DataType.Password), StringLength(32, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
