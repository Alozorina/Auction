using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole("User")
                {
                    Id = "36b84cb9-0986-470d-b4e8-a7d3cbb0d041",
                    ConcurrencyStamp = "0e0ad5f9-1381-4789-a31f-13b1918df4be"
                },
                new IdentityRole("Admin")
                {
                    Id = "334c125e-9f51-4a63-b249-73d44315e569",
                    ConcurrencyStamp = "daff2b33-d574-4cb9-b2f7-6c0f1de09045"
                }
            );
        }
    }
}