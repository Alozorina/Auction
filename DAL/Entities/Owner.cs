using System.Collections.Generic;

namespace DAL.Entities
{
    public class Owner : Person
    {
        public virtual ICollection<Item> Lots { get; set; }
    }
}
