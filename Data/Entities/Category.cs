using System.Collections.Generic;

namespace Data.Entities
{
    public class Category : Base
    {
        public string Name { get; set; }
        public virtual ICollection<ItemCategory> ItemCategories { get; set; }
    }
}