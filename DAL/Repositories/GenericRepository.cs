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

        /// <summary>
        /// Adds an entity to the database if an entity with this Id does not already exist
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous EF Core Add operation
        /// </returns>
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

        /// <summary>
        /// Removes an entity from the database if that entity Id exists in the database
        /// </summary>
        /// <returns>
        /// Remove operation from EF Core for the entity with given Id 
        /// </returns>
        public virtual void Delete(TEntity entity)
        {
            try
            {
                dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} DeleteByIdAsync function error", typeof(GenericRepository<TEntity>));
            }
        }

        /// <summary>
        /// Returns list of a sequence that satisfies a predicate or null
        /// </summary>
        /// <returns>
        /// Async Task. Task result returns the list of the sequence 
        /// that satisfies the condition in predicate, or null if no such element is found
        /// </returns>
        public async Task<List<TEntity>> FindByPredicateAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await GetAll().Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} FindAsync function error", typeof(GenericRepository<TEntity>));
                return null;
            }
        }

        /// <summary>
        /// Returns the only element of a sequence that satisfies a predicate or null
        /// </summary>
        /// <returns>
        /// Async Task. Task result returns the single element of the input sequence 
        /// that satisfies the condition in predicate, or null if no such element is found
        /// </returns>
        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await dbSet.FirstOrDefaultAsync(predicate);
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
        /// Async Task. Task result returns <IQueryable<Entity>>, or empty List<Entity> if an error occurred
        /// </returns>
        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return dbSet.AsQueryable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetAllAsync function error", typeof(GenericRepository<TEntity>));
                return new List<TEntity>().AsQueryable();
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
