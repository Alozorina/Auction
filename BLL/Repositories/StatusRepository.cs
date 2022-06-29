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
    public class StatusRepository : GenericRepository<Status>, IStatusRepository
    {
        public StatusRepository(AuctionDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<IEnumerable<Status>> GetAllWithDetailsAsync()
        {
            try
            {
                return await dbSet
                                .Include(s => s.Auctions)
                                .Include(s => s.Items)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAllWithDetailsAsync function error", typeof(StatusRepository));
                return await GetAllAsync();
            }
        }

        public async Task<Status> GetByIdWithDetailsAsync(int id)
        {
            try
            {
                return await dbSet
                                .Include(s => s.Auctions)
                                .Include(s => s.Items)
                                .SingleOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetByIdWithDetailsAsync function error", typeof(StatusRepository));
                return await GetByIdAsync(id);
            }
        }

        public override async Task UpdateAsync(Status entity)
        {
            try
            {
                var existingEntity = await dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (existingEntity != null)
                {
                    existingEntity.Name = entity.Name;
                    existingEntity.Auctions = entity.Auctions;
                    existingEntity.Items = entity.Items;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} UpdateAsync function error", typeof(StatusRepository));
            }
        }
    }
}
