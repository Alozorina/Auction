using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Configuration
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
                }
            );
        }
    }
}
