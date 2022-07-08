using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Configuration
{
    public class ItemPhotoConfiguration : IEntityTypeConfiguration<ItemPhoto>
    {
        public void Configure(EntityTypeBuilder<ItemPhoto> builder)
        {
            builder.HasOne(ip => ip.Item)
                .WithMany(i => i.ItemPhotos);

            builder.HasData(
                new ItemPhoto
                {
                    Id = 1,
                    Name = "steve-johnson-unsplash.jpg",
                    Path = "/images/",
                    ItemId = 1
                },
                new ItemPhoto
                {
                    Id = 2,
                    Name = "pexels-steve-johnson-1840624.jpg",
                    Path = "/images/",
                    ItemId = 2
                }
            );
        }
    }
}
