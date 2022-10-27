using DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IItemRepository : IRepository<Item>
    {
        IQueryable<Item> GetAllWithDetails();
        Task<Item> GetByIdWithDetailsAsync(int id);
        IQueryable<Item> GetAllPublicDetails();
    }
}
