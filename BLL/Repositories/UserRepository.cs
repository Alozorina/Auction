using BLL.Models;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AuctionDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async override Task<User> FirstOrDefaultAsync(Expression<Func<User, bool>> predicate)
        {
            try
            {
                return await dbSet
                    .Include(u => u.Role)
                    .SingleOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} FirstOrDefaultAsync function error", typeof(UserRepository));
                return null;
            }
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

                if (existingEntity != null)
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

        public async Task UpdatePersonalInfoAsync(int id, UserUpdatePersonalInfoModel model)
        {
            try
            {
                var existingEntity = await dbSet.FirstOrDefaultAsync(x => x.Id == id);

                if (existingEntity != null)
                {
                    existingEntity.FirstName = model.FirstName;
                    existingEntity.LastName = model.LastName;
                    existingEntity.Password = model.Password;
                    existingEntity.BirthDate = model.BirthDate;
                    existingEntity.Email = model.Email;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} UpdatePersonalInfoAsync function error", typeof(UserRepository));
            }
        }

        public async Task UpdateRoleId(int userId, int roleId)
        {
            try
            {
                var existingEntity = await dbSet.FirstOrDefaultAsync(x => x.Id == userId);

                if (existingEntity != null)
                {
                    existingEntity.RoleId = roleId;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} UpdateRoleId function error", typeof(UserRepository));
            }
        }
    }
}
