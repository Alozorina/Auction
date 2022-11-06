using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class ItemPhoto : Base
    {
        [Required]
        public string Path { get; set; }
        [Required]
        public int ItemId { get; set; }
    }
}
