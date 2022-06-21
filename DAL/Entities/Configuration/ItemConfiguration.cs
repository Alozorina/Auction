using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Configuration
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasOne(p => p.Owner)
                .WithMany(i => i.Lots)
                .HasForeignKey(i => i.OwnerId);

            builder.HasOne(p => p.Buyer)
                .WithMany(i => i.Purchases);

            builder.HasMany(b => b.ItemCategories);
            builder.HasData(
                new Item
                {
                    Id = 1,
                    Name = "Blue Marble",
                    AuctionId = 1,
                    CurrentBid = 0,
                    StartingPrice = 50m,
                    StatusId = 1,
                    OwnerId = "a7043b5b-bede-48d7-9d36-c2f632c40f0f"
                },
                 new Item
                 {
                     Id = 2,
                     Name = "Revolution",
                     AuctionId = 1,
                     CurrentBid = 0,
                     StartingPrice = 60m,
                     StatusId = 1,
                     OwnerId = "618ac316-948e-40de-bfe5-35abb551c95b",
                 }
                );
        }
    }
}
