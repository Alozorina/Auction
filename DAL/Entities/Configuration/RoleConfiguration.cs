using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Configuration
{
    public enum UserRoles
    {
        User = 1,
        Admin
    }
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId);

            builder.HasData(
                new Role
                {
                    Id = (int)UserRoles.User,
                    Name = UserRoles.User.ToString(),
                },
                new Role
                {
                    Id = (int)UserRoles.Admin,
                    Name = UserRoles.Admin.ToString(),
                }
            );
        }
    }
}