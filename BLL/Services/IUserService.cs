using BLL.Models;
using DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.Business.Services
{
    public interface IUserService
    {
        Task<User> AddAsync(UserRegistrationModel model);
        Task DeleteByIdAsync(int id);
        Task<User> GetUserWithDetailsByIdAsync(int id);
        Task<User> GetUserByIdAsync(int id);
        UserPersonalInfoModel MapToUserPersonalInfoFromUser(User user);
        Task<IEnumerable<UserPulicInfo>> GetPublicInfoAsync();
        Task<User> LoginAsync(string email, string password);
        Task UpdateAsync(User model);
        Task UpdatePersonalInfo(int id, UserPersonalInfoModel updateModel);
        Task UpdatePassword(User user, UserPassword userPassword);
        Task UpdateRoleAsync(int userId, int roleId);
    }
}