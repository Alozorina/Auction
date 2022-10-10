using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ItemCategoryRepository : GenericRepository<ItemCategory>, IItemCategoryRepository
    {
        public ItemCategoryRepository(AuctionDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task UpdateAsync(ItemCategory entity)
        {
            try
            {
                var existingEntity = await dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (existingEntity != null)
                {
                    existingEntity.CategoryId = entity.CategoryId;
                    existingEntity.Category = entity.Category;
                    existingEntity.Item = entity.Item;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} UpdateAsync function error", typeof(ItemCategoryRepository));
            }
        }
    }
}
