using System.Collections.Generic;

namespace DAL.Entities
{
    public class Status : Base
    {
        public string Name { get; set; }
        public virtual ICollection<Auction> Auctions { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}