using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Base
    {
        [Required]
        public int Id { get; set; }
        public Base()
        {
            if (Id == 0)
                Id = Guid.NewGuid().GetHashCode();
        }
    }
}
