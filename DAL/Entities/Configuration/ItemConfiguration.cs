using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

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
                    CurrentBid = 0,
                    StartingPrice = 50m,
                    StatusId = 1,
                    OwnerId = 1,
                    StartSaleDate = new DateTime(2022, 07, 22, 12, 00, 00),
                    EndSaleDate = new DateTime(2022, 08, 15, 22, 00, 00)
                },
                 new Item
                 {
                     Id = 2,
                     Name = "Revolution",
                     CurrentBid = 0,
                     StartingPrice = 60m,
                     StatusId = 1,
                     OwnerId = 2,
                     StartSaleDate = new DateTime(2022, 08, 12, 10, 00, 00),
                     EndSaleDate = new DateTime(2022, 08, 15, 12, 00, 00)
                 },
                 new Item
                 {
                     Id = 3,
                     Name = "Sunset",
                     CurrentBid = 0,
                     StartingPrice = 30m,
                     StatusId = 1,
                     OwnerId = 2,
                     StartSaleDate = new DateTime(2022, 07, 10, 10, 00, 00),
                     EndSaleDate = new DateTime(2022, 08, 15, 12, 00, 00)
                 },
                 new Item
                 {
                     Id = 4,
                     Name = "Spinning Around",
                     CurrentBid = 20,
                     StartingPrice = 5m,
                     StatusId = 5,
                     OwnerId = 6,
                     BuyerId = 11,
                     StartSaleDate = new DateTime(2022, 06, 14, 10, 00, 00),
                     EndSaleDate = new DateTime(2022, 08, 3, 12, 00, 00)
                 },
                 new Item
                 {
                     Id = 5,
                     Name = "Antarctica Is Changing",
                     CurrentBid = 0,
                     StartingPrice = 60m,
                     StatusId = 4,
                     OwnerId = 6,
                     StartSaleDate = new DateTime(2022, 07, 09, 10, 00, 00),
                     EndSaleDate = new DateTime(2022, 08, 10, 12, 00, 00)
                 }
                );
        }
    }
}
