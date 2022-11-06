using Auction.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IItemRepository ItemRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IItemCategoryRepository ItemCategoryRepository { get; }
        IStatusRepository StatusRepository { get; }
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        Task SaveAsync();
    }
}
