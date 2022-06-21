using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Configuration
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.HasMany(o => o.Lots)
                .WithOne(o => o.Owner);

            builder.HasData(
                new Owner
                {
                    Id = 1,
                    FirstName = "Jack",
                    LastName = "Bush",
                    Email = "jbush@mail.com"
                },
                new Owner
                {
                    Id = 2,
                    FirstName = "Monika",
                    LastName = "Lewinski",
                    Email = "lewinski@mail.com"
                }
            );
        }
    }
}
