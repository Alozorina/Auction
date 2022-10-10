using BLL.Models;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllWithDetailsAsync();
        Task<User> GetByIdWithDetailsAsync(int id);
        Task UpdateRole(int userId, Role role);
        void UpdatePassword(User user, UserPassword userPassword);
        Task<User> Login(string email, string password);
    }
}
