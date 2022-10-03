using BLL.Models;
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
        /// Gets all items from the database, maps them to <ItemPublicInfo> and makes the nested navigation properties null
        /// </summary>
        /// <returns>
        /// Async Task. Task result contains <List<ItemPublicInfo>> that contains elements from the input sequence
        /// </returns>
        public async Task<List<ItemPublicInfo>> GetAllPublicWithDetailsAsync(AutoMapper.IMapper mapper)
        {
            var items = await GetAllWithDetailsAsync();
            return items
                .Select(item =>
                {
                    var publicItem = mapper.Map<ItemPublicInfo>(item);
                    publicItem.ItemCategories = publicItem.ItemCategories
                    .Select(ic => new ItemCategory
                    {
                        Id = ic.Id,
                        CategoryId = ic.CategoryId,
                        ItemId = ic.ItemId,
                        Category = new Category
                        {
                            Id = ic.Id,
                            Name = ic.Category.Name
                        }
                    })
                    .ToList();
                    publicItem.Status = new Status()
                    {
                        Id = publicItem.Status.Id,
                        Name = publicItem.Status.Name
                    };
                    return publicItem;
                })
                .OrderBy(i => i.StartSaleDate)
                .ToList();
        }

        /// <summary>
        /// Gets all items from the database with navigation properties
        /// </summary>
        /// <returns>
        /// Async Task. Task result contains <List<ItemPublicInfo>> that contains elements from the input sequence
        /// </returns>
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

        public bool UpdateBidByIdAsync(Item item, ItemUpdateBid data)
        {
            try
            {
                if (item != null)
                {
                    item.CurrentBid = data.CurrentBid;
                    item.BuyerId = data.BuyerId;
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
