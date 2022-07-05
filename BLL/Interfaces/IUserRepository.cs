using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllWithDetailsAsync();
        Task<User> GetByIdWithDetailsAsync(int id);
        Task UpdateRoleId(int userId, int roleId);
    }
}
