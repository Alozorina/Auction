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
                            .Include(i => i.Auction)
                            .Include(i => i.Status)
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
                            .Include(i => i.Auction)
                            .Include(i => i.Status)
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
                    existingEntity.Status = entity.Status;
                    existingEntity.StatusId = entity.StatusId;
                    existingEntity.AuctionId = entity.AuctionId;
                    existingEntity.Auction = entity.Auction;
                    existingEntity.Buyer = entity.Buyer;
                    existingEntity.Owner = entity.Owner;
                    existingEntity.OwnerId = entity.OwnerId;
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
