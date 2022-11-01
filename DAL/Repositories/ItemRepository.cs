using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        public ItemRepository(AuctionDbContext context, ILogger logger) : base(context, logger)
        {
        }
        /// <summary>
        /// Gets all items from the database with navigation properties
        /// </summary>
        /// <remarks> 
        /// Uses no tracking behavior
        /// </remarks>
        /// <returns>
        /// Async Task. Task result contains List<Item> that contains elements from the input sequence
        /// </returns>
        public async Task<List<Item>> GetAllWithDetails()
        {
            try
            {
                return await dbSet
                    .AsNoTracking()
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
                _logger.LogError(ex, "{Repo} GetAllWithDetails function error", typeof(ItemRepository));
                return await GetAll().AsNoTracking().ToListAsync();
            }
        }

        /// <summary>
        /// Gets a single Item from the database with navigation properties
        /// </summary>
        /// <returns>
        /// Async Task. Task result contains a single <Item> with the given id.
        /// </returns>
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

        /// <summary>
        /// Gets all items from the database with navigation properties
        /// </summary>
        /// <returns>
        /// IQueryable<Item> that contains elements from the input sequence
        /// </returns>
        public IQueryable<Item> GetAllPublicDetails()
        {
            try
            {
                return dbSet
                    .Include(i => i.ItemCategories)
                        .ThenInclude(ic => ic.Category)
                    .Include(i => i.Status)
                    .Include(i => i.ItemPhotos)
                    .AsQueryable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAllPublicDetails function error", typeof(ItemRepository));
                return GetAll();
            }
        }

        /// <summary>
        /// Gets Item with id from given <Item> entity and updates non-navigation properties if item is found. 
        /// </summary>
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
    }
}
