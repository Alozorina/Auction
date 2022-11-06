using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IStatusRepository : IRepository<Status>
    {
        Task<IEnumerable<Status>> GetAllWithDetailsAsync();
        Task<Status> GetByIdWithDetailsAsync(int id);
    }
}
