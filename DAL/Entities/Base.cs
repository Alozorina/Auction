using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Base
    {
        [Required]
        public int Id { get; set; }
    }
}
