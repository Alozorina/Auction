using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property("Name").IsRequired();
            builder.HasData(
                new Category
                {
                    Id = 1,
                    Name = "Painting",
                },
                new Category
                {
                    Id = 2,
                    Name = "Steve Johnson",
                },
                new Category
                {
                    Id = 3,
                    Name = "Sculpture",
                },
                new Category
                {
                    Id = 4,
                    Name = "Ceramics",
                },
                new Category
                {
                    Id = 5,
                    Name = "Chinese Art",
                },
                new Category
                {
                    Id = 6,
                    Name = "Porcelain",
                },
                new Category
                {
                    Id = 7,
                    Name = "Jewelry",
                }
                );
        }
    }
}
