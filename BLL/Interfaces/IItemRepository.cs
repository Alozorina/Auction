using BLL.Models;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IItemRepository : IRepository<Item>
    {
        Task<bool> UpdateBidByIdAsync(int id, decimal newBid);
        Task<IEnumerable<Item>> GetAllWithDetailsAsync();
        Task<Item> GetByIdWithDetailsAsync(int id);
        Task<List<ItemPublicInfo>> GetAllPublicWithDetailsAsync(AutoMapper.IMapper mapper);
    }
}
