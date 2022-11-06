using System.Collections.Generic;

namespace Data.Entities
{
    public class Status : Base
    {
        public string Name { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}