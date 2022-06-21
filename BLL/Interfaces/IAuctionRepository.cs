using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAuctionRepository : IRepository<Auction>
    {
        Task<bool> AddItemAsync(int auctionId, Item entity);
        Task<IEnumerable<Auction>> GetAllWithDetailsAsync();
        Task<Auction> GetByIdWithDetailsAsync(int id);
    }
}
