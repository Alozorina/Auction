using BLL;
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
    public abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : Base
    {
        internal DbSet<TEntity> dbSet;
        public readonly ILogger _logger;

        protected GenericRepository(AuctionDbContext context, ILogger logger)
        {
            dbSet = context.Set<TEntity>();
            _logger = logger;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} AddAsync function error", typeof(GenericRepository<TEntity>));
            }
        }

        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                var exist = await dbSet.FirstOrDefaultAsync(x => x.Id == id);

                if (exist == null)
                    throw new AuctionException("Wrong Id");

                dbSet.Remove(exist);
            }
            catch (AuctionException ex)
            {
                _logger.LogError(ex, "{Repo} DeleteByIdAsync function error", typeof(GenericRepository<TEntity>));
            }
        }

        public async Task<IEnumerable<TEntity>> FindByPredicateAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await dbSet.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} FindAsync function error", typeof(GenericRepository<TEntity>));
                return null;
            }
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await dbSet.SingleOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} FirstOrDefaultAsync function error", typeof(GenericRepository<TEntity>));
                return null;
            }
        }

        /// <summary>
        /// Gets all elements in database
        /// </summary>
        /// <returns>
        /// Async Task. Task result returns <IEnumerable<Entity>>, or empty List<Entity> if an error occurred
        /// </returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAllAsync function error", typeof(GenericRepository<TEntity>));
                return new List<TEntity>();
            }
        }

        /// <summary>
        /// Gets info about entity without details
        /// </summary>
        /// <remarks> 
        /// Navigation properties will be null
        /// </remarks>
        /// <returns>
        /// Async Task. Task result contains the single element corresponding to the incoming ID, or null if an error occurred
        /// </returns>
        public async Task<TEntity> GetByIdAsync(int id)
        {
            try
            {
                return await dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetByIdAsync function error", typeof(GenericRepository<TEntity>));
                return null;
            }
        }

        public abstract Task UpdateAsync(TEntity entity);
    }
}
