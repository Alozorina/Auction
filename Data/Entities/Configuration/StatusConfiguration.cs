using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Data.Entities.Configuration
{
    public enum Statuses
    {
        OnApproval = 1,
        Upcoming,
        Rejected,
        Open,
        Closed
    }
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasMany(i => i.Items)
                .WithOne(item => item.Status);

            builder.HasData(new List<Status>()
            {
                new Status
                {
                    Id = (int)Statuses.OnApproval,
                    Name = Statuses.OnApproval.ToString(),
                },
                new Status
                {
                    Id = (int)Statuses.Upcoming,
                    Name = Statuses.Upcoming.ToString(),
                },
                new Status
                {
                    Id = (int)Statuses.Rejected,
                    Name = Statuses.Rejected.ToString(),
                },
                new Status
                {
                    Id = (int)Statuses.Open,
                    Name = Statuses.Open.ToString(),
                },
                new Status
                {
                    Id = (int)Statuses.Closed,
                    Name = Statuses.Closed.ToString(),
                }
            });

            builder.Property("Name").IsRequired();
        }
    }
}
