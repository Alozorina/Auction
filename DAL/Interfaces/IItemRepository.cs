using DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IItemRepository : IRepository<Item>
    {
        Task<List<Item>> GetAllWithDetails();
        Task<Item> GetByIdWithDetailsAsync(int id);
        IQueryable<Item> GetAllPublicDetails();
    }
}
