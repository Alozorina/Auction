using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace DAL.Entities.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public List<User> Users { get; set; }
        public void Configure(EntityTypeBuilder<User> builder)
        {
            Users = new List<User>()
             {
                 new User
                 {
                     Id = 1,
                     FirstName = "Jane",
                     LastName = "Doe",
                     Email = "janemail@mail.com",
                     Password = "passwordJane",
                     BirthDate = new DateTime(2000, 12, 12),
                     RoleId = 2
                 },
                 new User
                 {
                     Id = 2,
                     FirstName = "John",
                     LastName = "Doe",
                     Email = "johnmail@mail.com",
                     Password = "passwordJohn",
                     BirthDate = new DateTime(2000, 2, 2),
                     RoleId = 1,
                 }
             };
            builder.HasData(Users);
        }
    }
}
