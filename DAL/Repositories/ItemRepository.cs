using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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
        /// <returns>
        /// IQueryable<Item> that contains elements from the input sequence
        /// </returns>
        public IQueryable<Item> GetAllWithDetails()
        {
            try
            {
                return dbSet
                    .Include(i => i.Owner)
                    .Include(i => i.Buyer)
                    .Include(i => i.ItemCategories)
                        .ThenInclude(ic => ic.Category)
                    .Include(i => i.Status)
                    .Include(i => i.ItemPhotos)
                    .AsQueryable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAllWithDetailsAsync function error", typeof(ItemRepository));
                return GetAll();
            }
        }

        /// <summary>
        /// Gets all items from the database with navigation properties
        /// </summary>
        /// <returns>
        /// Async Task. Task result contains a single <Item> that satisfies the value of "id"
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
                _logger.LogError(ex, "{Repo} GetAllWithDetailsAsync function error", typeof(ItemRepository));
                return GetAll();
            }
        }

        /// <summary>
        /// Gets all items from the database with navigation properties
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
