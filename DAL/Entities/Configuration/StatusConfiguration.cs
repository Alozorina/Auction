using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace DAL.Entities.Configuration
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public List<Status> Statuses { get; set; }

        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasMany(i => i.Items)
                .WithOne(item => item.Status);

            Statuses = new List<Status>()
            {
                new Status
                {
                    Id = 1,
                    Name = "On Approval",
                },
                new Status
                {
                    Id = 2,
                    Name = "Approved",
                },
                new Status
                {
                    Id = 3,
                    Name = "Rejected",
                },
                new Status
                {
                    Id = 4,
                    Name = "Open",
                },
                new Status
                {
                    Id = 5,
                    Name = "Closed",
                }
            };

            builder.Property("Name").IsRequired();
            builder.HasData(Statuses);
        }
    }
}
