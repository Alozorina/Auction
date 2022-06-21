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
                     Id = "a7043b5b-bede-48d7-9d36-c2f632c40f0f",
                     FirstName = "Jane",
                     LastName = "Doe",
                     Email = "janemail@mail.com",
                     PhoneNumber = "123-456-789",
                     BirthDate = new DateTime(2000, 12, 12),
                     ConcurrencyStamp = "4174f09a-a3c4-4651-a358-ae4b367ade48",
                     SecurityStamp = "cbec5110-5c11-420d-9606-3af0bcf66676"
                 },
                 new User
                 {
                     Id = "618ac316-948e-40de-bfe5-35abb551c95b",
                     FirstName = "John",
                     LastName = "Doe",
                     Email = "johnmail@mail.com",
                     PhoneNumber = "143-456-789",
                     BirthDate = new DateTime(2000, 2, 2),
                     ConcurrencyStamp = "526ca0e4-560d-4ab7-9b5b-93dc9a56783a",
                     SecurityStamp = "ed32f315-adec-4805-b397-f5ac3792dd44"
                 }
             };
            builder.HasData(Users);
        }
    }
}
