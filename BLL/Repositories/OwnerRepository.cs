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
    public class OwnerRepository : GenericRepository<Owner>, IOwnerRepository
    {
        public OwnerRepository(AuctionDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<IEnumerable<Owner>> GetAllWithDetailsAsync()
        {
            try
            {
                return await dbSet
                            .Include(o => o.Lots)
                            .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAllWithDetailsAsync function error", typeof(OwnerRepository));
                return await GetAllAsync();
            }
        }

        public async Task<Owner> GetByIdWithDetailsAsync(int id)
        {
            try
            {
                return await dbSet
                            .Include(o => o.Lots)
                            .SingleOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetByIdWithDetailsAsync function error", typeof(OwnerRepository));
                return await GetByIdAsync(id);
            }
        }

        public override async Task UpdateAsync(Owner entity)
        {
            try
            {
                var existingEntity = await dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (existingEntity == null)
                    dbSet.Add(entity);
                else
                {
                    existingEntity.Id = entity.Id;
                    existingEntity.FirstName = entity.FirstName;
                    existingEntity.LastName = entity.LastName;
                    existingEntity.Email = entity.Email;
                    existingEntity.Lots = entity.Lots;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} UpdateAsync function error", typeof(OwnerRepository));
            }
        }
    }
}
