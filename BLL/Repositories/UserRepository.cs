using BLL;
using BLL.Validation;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AuctionDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task AddAsync(User user)
        {
            var users = await GetAllAsync();
            if (UserValidation.IsEmailExists(users, user.Email))
                throw new AuctionException("This email is being used by another user");

            await dbSet.AddAsync(user);
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

        public override async Task UpdateAsync(User model)
        {
            var existingEntity = await dbSet.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (existingEntity != null)
            {
                var users = await GetAllAsync();
                bool isEmailExists = UserValidation.IsEmailCouldBeUpdated(users, existingEntity.Email, model.Email);
                bool isPasswordMatches = UserValidation.IsClientPasswordMatches(existingEntity.Password, model.Password);
                bool isModelHasNullProperty = UserValidation.IsModelHasNullProperty(model);
                if (isEmailExists || !isPasswordMatches)
                    throw new AuctionException("Invalid input");

                if (isModelHasNullProperty)
                    throw new AuctionException("One of the required properties is null");
            }
            else
            {
                throw new AuctionException("Id doesnt't exist");
            }
            existingEntity.FirstName = model.FirstName;
            existingEntity.LastName = model.LastName;
            existingEntity.Password = model.Password;
            existingEntity.BirthDate = model.BirthDate;
            existingEntity.Email = model.Email;
            existingEntity.RoleId = model.RoleId;
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
