using System.Collections.Generic;

namespace Data.Entities
{
    public class Role : Base
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}