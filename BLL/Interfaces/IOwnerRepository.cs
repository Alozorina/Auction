using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IOwnerRepository : IRepository<Owner>
    {
        Task<IEnumerable<Owner>> GetAllWithDetailsAsync();
        Task<Owner> GetByIdWithDetailsAsync(int id);
    }
}
