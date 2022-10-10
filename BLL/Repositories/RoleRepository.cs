using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(AuctionDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task UpdateAsync(Role entity)
        {
            try
            {
                var existingEntity = await dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (existingEntity != null)
                {
                    existingEntity.Name = entity.Name;
                    existingEntity.Users = entity.Users;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} UpdateAsync function error", typeof(RoleRepository));
            }
        }
    }
}
