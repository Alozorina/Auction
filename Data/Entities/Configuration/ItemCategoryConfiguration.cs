using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities.Configuration
{
    public class ItemCategoryConfiguration : IEntityTypeConfiguration<ItemCategory>
    {
        public void Configure(EntityTypeBuilder<ItemCategory> builder)
        {
            builder.HasData(
                new ItemCategory()
                {
                    Id = 1,
                    CategoryId = 1,
                    ItemId = 1
                },
                new ItemCategory()
                {
                    Id = 2,
                    CategoryId = 1,
                    ItemId = 2
                },
                new ItemCategory()
                {
                    Id = 3,
                    CategoryId = 2,
                    ItemId = 2
                },
                new ItemCategory()
                {
                    Id = 4,
                    CategoryId = 2,
                    ItemId = 1
                },
                new ItemCategory()
                {
                    Id = 5,
                    CategoryId = 2,
                    ItemId = 3
                },
                new ItemCategory()
                {
                    Id = 6,
                    CategoryId = 2,
                    ItemId = 4
                },
                new ItemCategory()
                {
                    Id = 7,
                    CategoryId = 2,
                    ItemId = 5
                },
                new ItemCategory()
                {
                    Id = 8,
                    CategoryId = 1,
                    ItemId = 2
                },
                new ItemCategory()
                {
                    Id = 9,
                    CategoryId = 1,
                    ItemId = 1
                },
                new ItemCategory()
                {
                    Id = 10,
                    CategoryId = 1,
                    ItemId = 3
                },
                new ItemCategory()
                {
                    Id = 11,
                    CategoryId = 1,
                    ItemId = 4
                },
                new ItemCategory()
                {
                    Id = 12,
                    CategoryId = 1,
                    ItemId = 5
                }
            );
        }
    }
}
