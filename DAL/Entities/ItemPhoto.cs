using System.Collections.Generic;

namespace DAL.Entities
{
    public class ItemPhoto : Base
    {
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public ICollection<string> Paths { get; set; }
    }
}
