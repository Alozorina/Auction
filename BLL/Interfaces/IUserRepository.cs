using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllWithDetailsAsync();
        Task<User> GetByIdWithDetailsAsync(int id);
        Task UpdateRole(User model);
        Task UpdatePassword(User model);
        Task<User> Login(string email, string password);
    }
}
