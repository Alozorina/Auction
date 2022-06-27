using System;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IItemRepository ItemRepository { get; }
        IAuctionCategoryRepository AuctionCategoryRepository { get; }
        IAuctionRepository AuctionRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IItemCategoryRepository ItemCategoryRepository { get; }
        IStatusRepository StatusRepository { get; }
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        Task SaveAsync();
    }
}
