using Auction.Data.Interfaces;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HashHandler = BCrypt.Net.BCrypt;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AuctionDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<User> GetUserWithRoleAsync(Expression<Func<User, bool>> predicate)
        {
            try
            {
                return await dbSet
                    .Include(u => u.Role)
                    .AsNoTracking()
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
                                .Include(u => u.Lots)
                                .Include(u => u.Role)
                                .AsNoTracking()
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAllWithDetailsAsync function error", typeof(UserRepository));
                return null;
            }
        }

        public async Task<User> GetByIdWithDetailsAsync(int id)
        {
            try
            {
                return await dbSet
                                .Include(u => u.Purchases).ThenInclude(l => l.ItemCategories).ThenInclude(ic => ic.Category)
                                .Include(u => u.Lots).ThenInclude(l => l.ItemCategories).ThenInclude(ic => ic.Category)
                                .Include(u => u.Lots).ThenInclude(l => l.Status)
                                .Include(u => u.Role)
                                .AsNoTracking()
                                .SingleOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetByIdWithDetailsAsync function error", typeof(UserRepository));
                return await GetByIdAsync(id);
            }
        }

        public override async Task UpdateAsync(User model)
        {
            var existingEntity = await dbSet.FirstOrDefaultAsync(x => x.Id == model.Id);

            existingEntity.FirstName = model.FirstName;
            existingEntity.LastName = model.LastName;
            existingEntity.Password = HashHandler.HashPassword(model.Password);
            existingEntity.BirthDate = model.BirthDate;
            existingEntity.Email = model.Email;
            existingEntity.RoleId = model.RoleId;
        }


    }
}
