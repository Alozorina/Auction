using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuctionDbContext _context;
        public IItemRepository ItemRepository { get; private set; }

        public ICategoryRepository CategoryRepository { get; private set; }

        public IItemCategoryRepository ItemCategoryRepository { get; private set; }

        public IStatusRepository StatusRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public IRoleRepository RoleRepository { get; private set; }


        public UnitOfWork(AuctionDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            ILogger _logger = loggerFactory.CreateLogger("logs");
            ItemRepository = new ItemRepository(context, _logger);
            CategoryRepository = new CategoryRepository(context, _logger);
            ItemCategoryRepository = new ItemCategoryRepository(context, _logger);
            StatusRepository = new StatusRepository(context, _logger);
            RoleRepository = new RoleRepository(context, _logger);
            UserRepository = new UserRepository(context, _logger);
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
