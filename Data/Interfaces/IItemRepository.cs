using Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IItemRepository : IRepository<Item>
    {
        Task<List<Item>> GetAllWithDetails();
        Task<Item> GetByIdWithDetailsAsync(int id);
        IQueryable<Item> GetAllPublicDetails();
    }
}
