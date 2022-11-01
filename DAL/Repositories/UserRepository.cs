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

        /// <summary>
        /// Gets User from the database with Role navigation property
        /// </summary>
        /// <returns>
        /// Async Task. Task result contains a single <User> that satisfies the predicate
        /// </returns>
        public async Task<User> GetUserWithRoleAsync(Expression<Func<User, bool>> predicate)
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

        /// <summary>
        /// Gets all users from the database with navigation properties
        /// </summary>
        /// <remarks> 
        /// Uses no tracking behavior
        /// </remarks>
        /// <returns>
        /// Async Task. Task result contains IEnumerable<User> that contains elements from the input sequence
        /// </returns>
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

        /// <summary>
        /// Gets a single User from the database with all nested navigation properties.
        /// </summary>
        /// <returns>
        /// Async Task. Task result contains a single <User> with the given id.
        /// </returns>
        public async Task<User> GetByIdWithDetailsAsync(int id)
        {
            try
            {
                return await dbSet
                                .Include(u => u.Purchases).ThenInclude(l => l.ItemCategories).ThenInclude(ic => ic.Category)
                                .Include(u => u.Lots).ThenInclude(l => l.ItemCategories).ThenInclude(ic => ic.Category)
                                .Include(u => u.Lots).ThenInclude(l => l.Status)
                                .Include(u => u.Role)
                                .SingleOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetByIdWithDetailsAsync function error", typeof(UserRepository));
                return await GetByIdAsync(id);
            }
        }

        /// <summary>
        /// Gets User with id from given <User> entity and updates non-navigation properties if user is found. 
        /// </summary>
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
