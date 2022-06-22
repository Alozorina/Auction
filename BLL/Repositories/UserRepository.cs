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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AuctionDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<IEnumerable<User>> GetAllWithDetailsAsync()
        {
            try
            {
                return await dbSet
                                .Include(u => u.Purchases)
                                .Include(u => u.Role)
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAllWithDetailsAsync function error", typeof(UserRepository));
                return await GetAllAsync();
            }
        }

        public async Task<User> GetByIdWithDetailsAsync(int id)
        {
            try
            {
                return await dbSet
                                .Include(u => u.Purchases)
                                .Include(u => u.Role)
                                .SingleOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetByIdWithDetailsAsync function error", typeof(UserRepository));
                return await GetByIdAsync(id);
            }
        }

        public async Task<bool> IsEmailExists(string email)
        {
            var users = await GetAllAsync();
            return users.Any(u => u.Email == email);
        }

        public override async Task UpdateAsync(User entity)
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
                    existingEntity.Purchases = entity.Purchases;
                    existingEntity.Password = entity.Password;
                    existingEntity.BirthDate = entity.BirthDate;
                    existingEntity.Email = entity.Email;
                    existingEntity.RoleId = entity.RoleId;
                    existingEntity.Role = entity.Role;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} UpdateAsync function error", typeof(UserRepository));
            }
        }
    }
}
