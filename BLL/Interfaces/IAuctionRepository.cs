using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAuctionRepository : IRepository<AuctionEntity>
    {
        Task<bool> AddItemAsync(int auctionId, Item entity);
        Task<IEnumerable<AuctionEntity>> GetAllWithDetailsAsync();
        Task<AuctionEntity> GetByIdWithDetailsAsync(int id);
    }
}
