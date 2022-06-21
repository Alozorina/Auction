using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AuctionDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task UpdateAsync(Category entity)
        {
            try
            {
                var existingEntity = await dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (existingEntity == null)
                    dbSet.Add(entity);
                else
                {
                    existingEntity.Id = entity.Id;
                    existingEntity.Name = entity.Name;
                    existingEntity.AuctionCategories = entity.AuctionCategories;
                    existingEntity.ItemCategories = entity.ItemCategories;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} UpdateAsync function error", typeof(CategoryRepository));
            }
        }
    }
}
