using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Data.Entities.Configuration
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasOne(p => p.Owner)
                .WithMany(i => i.Lots)
                .HasForeignKey(i => i.OwnerId);

            builder.HasMany(i => i.ItemPhotos);

            builder.HasOne(p => p.Buyer)
                .WithMany(i => i.Purchases);

            builder.HasMany(b => b.ItemCategories);
            builder.HasData(
                new Item
                {
                    Id = 1,
                    Name = "Blue Marble",
                    CreatedBy = "Steve Johnson",
                    CurrentBid = 0,
                    StartingPrice = 50m,
                    StatusId = 2,
                    OwnerId = 1,
                    StartSaleDate = new DateTime(2023, 05, 22, 12, 00, 00),
                    EndSaleDate = new DateTime(2023, 08, 15, 22, 00, 00),
                    Description = "Following the success of the inaugural edition in March 2021, " +
                    "consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors" +
                    " auction this July. Running the gamut of the avant-garde from the late 19th century through " +
                    "to artists working today, the sale presents exceptional artworks by artists who dared to " +
                    "innovate and experiment over the course of 150 years."
                },
                 new Item
                 {
                     Id = 2,
                     Name = "Revolution",
                     CreatedBy = "Steve Johnson",
                     CurrentBid = 0,
                     StartingPrice = 60m,
                     StatusId = 2,
                     OwnerId = 2,
                     StartSaleDate = new DateTime(2022, 12, 12, 10, 00, 00),
                     EndSaleDate = new DateTime(2023, 08, 15, 12, 00, 00),
                     Description = "Following the success of the inaugural edition in March 2021, " +
                    "consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors" +
                    " auction this July. Running the gamut of the avant-garde from the late 19th century through " +
                    "to artists working today, the sale presents exceptional artworks by artists who dared to " +
                    "innovate and experiment over the course of 150 years."
                 },
                 new Item
                 {
                     Id = 3,
                     Name = "Sunset",
                     CreatedBy = "Steve Johnson",
                     CurrentBid = 40,
                     StartingPrice = 30m,
                     StatusId = 4,
                     OwnerId = 2,
                     StartSaleDate = new DateTime(2023, 03, 09, 10, 00, 00),
                     EndSaleDate = new DateTime(2023, 08, 15, 12, 00, 00),
                     Description = "Following the success of the inaugural edition in March 2021, " +
                    "consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors" +
                    " auction this July. Running the gamut of the avant-garde from the late 19th century through " +
                    "to artists working today, the sale presents exceptional artworks by artists who dared to " +
                    "innovate and experiment over the course of 150 years."
                 },
                 new Item
                 {
                     Id = 4,
                     Name = "Spinning Around",
                     CreatedBy = "Steve Johnson",
                     CurrentBid = 20,
                     StartingPrice = 5m,
                     StatusId = 5,
                     OwnerId = 6,
                     BuyerId = 11,
                     StartSaleDate = new DateTime(2022, 06, 14, 10, 00, 00),
                     EndSaleDate = new DateTime(2022, 08, 3, 12, 00, 00),
                     Description = "Following the success of the inaugural edition in March 2021, " +
                    "consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors" +
                    " auction this July. Running the gamut of the avant-garde from the late 19th century through " +
                    "to artists working today, the sale presents exceptional artworks by artists who dared to " +
                    "innovate and experiment over the course of 150 years."
                 },
                 new Item
                 {
                     Id = 5,
                     Name = "Antarctica Is Changing",
                     CreatedBy = "Steve Johnson",
                     CurrentBid = 70,
                     StartingPrice = 60m,
                     StatusId = 4,
                     OwnerId = 6,
                     StartSaleDate = new DateTime(2022, 07, 09, 10, 00, 00),
                     EndSaleDate = new DateTime(2023, 08, 10, 12, 00, 00),
                     Description = "Following the success of the inaugural edition in March 2021, " +
                    "consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors" +
                    " auction this July. Running the gamut of the avant-garde from the late 19th century through " +
                    "to artists working today, the sale presents exceptional artworks by artists who dared to " +
                    "innovate and experiment over the course of 150 years."
                 },
                 new Item
                 {
                     Id = 6,
                     Name = "Green Hills",
                     CreatedBy = "Jesse Zheng",
                     CurrentBid = 70,
                     StartingPrice = 60m,
                     StatusId = 4,
                     OwnerId = 6,
                     StartSaleDate = new DateTime(2022, 07, 09, 10, 00, 00),
                     EndSaleDate = new DateTime(2023, 08, 10, 12, 00, 00),
                 },
                 new Item
                 {
                     Id = 7,
                     Name = "Black Gold",
                     CreatedBy = "Pawel Czerwinski",
                     CurrentBid = 0,
                     StartingPrice = 60m,
                     StatusId = 2,
                     OwnerId = 11,
                     StartSaleDate = new DateTime(2023, 08, 09, 11, 00, 00),
                     EndSaleDate = new DateTime(2023, 08, 19, 12, 00, 00),
                 }
                );
        }
    }
}
