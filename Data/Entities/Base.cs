using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Base
    {
        [Required]
        public int Id { get; set; }
    }
}
