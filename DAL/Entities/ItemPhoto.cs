using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class ItemPhoto : Base
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
