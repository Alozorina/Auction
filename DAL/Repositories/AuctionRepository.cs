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
    public class AuctionRepository : GenericRepository<Auction>, IAuctionRepository
    {
        public AuctionRepository(AuctionDbContext context, ILogger logger) : base(context, logger) { }

        public async Task<bool> AddItemAsync(int auctionId, Item entity)
        {
            try
            {
                var auction = await dbSet.FindAsync(auctionId);
                if (auction == null)
                    return false;

                auction.Items.Add(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} AddItemAsync function error", typeof(AuctionRepository));
                return false;
            }
        }

        public async Task<IEnumerable<Auction>> GetAllWithDetailsAsync()
        {
            try
            {
                return await dbSet
                                .Include(a => a.Items)
                                .Include(a => a.Status)
                                .Include(a => a.AuctionCategories)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAllWithDetailsAsync function error", typeof(AuctionRepository));
                return await GetAllAsync();
            }
        }

        public async Task<Auction> GetByIdWithDetailsAsync(int id)
        {
            try
            {
                return await dbSet
                                .Include(a => a.Items)
                                .Include(a => a.Status)
                                .Include(a => a.AuctionCategories)
                                .SingleOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetByIdWithDetailsAsync function error", typeof(AuctionRepository));
                return await GetByIdAsync(id);
            }
        }

        public override async Task UpdateAsync(Auction entity)
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
                    existingEntity.Description = entity.Description;
                    existingEntity.Status = entity.Status;
                    existingEntity.StatusId = entity.StatusId;
                    existingEntity.AuctionCategories = entity.AuctionCategories;
                    existingEntity.EndSaleDate = entity.EndSaleDate;
                    existingEntity.StartSaleDate = entity.StartSaleDate;
                    existingEntity.Items = entity.Items;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} UpdateAsync function error", typeof(AuctionRepository));
            }
        }
    }
}
