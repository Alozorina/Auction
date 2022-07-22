using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Configuration
{
    public class ItemPhotoConfiguration : IEntityTypeConfiguration<ItemPhoto>
    {
        public void Configure(EntityTypeBuilder<ItemPhoto> builder)
        {
            builder.HasData(
                new ItemPhoto
                {
                    Id = 1,
                    Path = "steve-johnson-unsplash.jpg",
                    ItemId = 1
                },
                new ItemPhoto
                {
                    Id = 2,
                    Path = "pexels-steve-johnson-1840624.jpg",
                    ItemId = 2
                },
                new ItemPhoto
                {
                    Id = 3,
                    Path = "pexels-steve-johnson-1174000.jpg",
                    ItemId = 3
                },
                new ItemPhoto
                {
                    Id = 4,
                    Path = "pexels-steve-johnson-1286632.jpg",
                    ItemId = 4
                },
                new ItemPhoto
                {
                    Id = 5,
                    Path = "steve-johnson-RzykwoNjoLw-unsplash.jpg",
                    ItemId = 5
                },
                new ItemPhoto
                {
                    Id = 6,
                    Path = "steve-johnson-RzykwoNjoLw-unsplash-mockup.jpg",
                    ItemId = 5
                },
                new ItemPhoto
                {
                    Id = 7,
                    Path = "pexels-jesse-zheng-732548.jpg",
                    ItemId = 6
                },
                new ItemPhoto
                {
                    Id = 8,
                    Path = "pawel-czerwinski-xubOAAKUwXc-unsplash.jpg",
                    ItemId = 7
                }
            );
        }
    }
}
