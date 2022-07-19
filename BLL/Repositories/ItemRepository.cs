using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        public ItemRepository(AuctionDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<IEnumerable<Item>> GetAllWithDetailsAsync()
        {
            try
            {
                return await dbSet
                            .Include(i => i.Owner)
                            .Include(i => i.Buyer)
                            .Include(i => i.ItemCategories)
                                .ThenInclude(ic => ic.Category)
                            .Include(i => i.Status)
                            .Include(i => i.ItemPhotos)
                            .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAllWithDetailsAsync function error", typeof(ItemRepository));
                return await GetAllAsync();
            }
        }

        public async Task<Item> GetByIdWithDetailsAsync(int id)
        {
            try
            {
                return await dbSet
                            .Include(i => i.Owner)
                            .Include(i => i.Buyer)
                            .Include(i => i.ItemCategories)
                                .ThenInclude(ic => ic.Category)
                            .Include(i => i.Status)
                            .Include(i => i.ItemPhotos)
                            .SingleOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetByIdWithDetailsAsync function error", typeof(ItemRepository));
                return await GetByIdAsync(id);
            }
        }

        public override async Task UpdateAsync(Item entity)
        {
            try
            {
                var existingEntity = await dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (existingEntity != null)
                {
                    existingEntity.Name = entity.Name;
                    existingEntity.Description = entity.Description;
                    existingEntity.StatusId = entity.StatusId;
                    existingEntity.OwnerId = entity.OwnerId;
                    existingEntity.BuyerId = entity.BuyerId;
                    existingEntity.CurrentBid = entity.CurrentBid;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} UpdateAsync function error", typeof(ItemRepository));
            }
        }

        public async Task<bool> UpdateBidByIdAsync(int id, decimal newBid)
        {
            try
            {
                var existingEntity = await dbSet.FindAsync(id);
                if (existingEntity != null)
                {
                    existingEntity.CurrentBid = newBid;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} UpdateBidAsync function error", typeof(ItemRepository));
                return false;
            }
        }
    }
}
