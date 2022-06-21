using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Configuration
{
    public class AuctionCategoryConfiguration : IEntityTypeConfiguration<AuctionCategory>
    {
        public void Configure(EntityTypeBuilder<AuctionCategory> builder)
        {
            builder.HasData(
                new AuctionCategory()
                {
                    Id = 1,
                    CategoryId = 1,
                    AuctionId = 1
                },
                new AuctionCategory()
                {
                    Id = 2,
                    CategoryId = 2,
                    AuctionId = 1
                }
            );
        }
    }
}
