using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DAL.Entities.Configuration
{
    public class AuctionConfiguration : IEntityTypeConfiguration<AuctionEntity>
    {
        public void Configure(EntityTypeBuilder<AuctionEntity> builder)
        {
            builder.HasMany(i => i.Items)
                .WithOne(a => a.Auction)
                .HasForeignKey(i => i.AuctionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(c => c.AuctionCategories);
            builder.HasData(
                new AuctionEntity
                {
                    Id = 1,
                    Name = "Steve Johnson's Bright Colors",
                    StartSaleDate = new DateTime(2022, 6, 27, 14, 0, 0),
                    EndSaleDate = new DateTime(2022, 7, 1, 20, 0, 0),
                    StatusId = 1,
                    Description = "Following the success of the inaugural edition in March 2021, " +
                    "consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors" +
                    " auction this June. Running the gamut of the avant-garde from the late 19th century through " +
                    "to artists working today, the sale presents exceptional artworks by artists who dared to " +
                    "innovate and experiment over the course of 150 years.",
                }
             );
        }
    }
}
