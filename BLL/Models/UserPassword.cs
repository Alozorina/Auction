using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class UserPassword
    {
        [Required, DataType(DataType.Password), StringLength(32, MinimumLength = 6)]
        public string OldPassword { get; set; }
        [Required, DataType(DataType.Password), StringLength(32, MinimumLength = 6)]
        public string NewPassword { get; set; }
    }
}
