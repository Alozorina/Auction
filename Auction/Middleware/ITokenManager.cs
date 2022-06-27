using DAL.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Auction.Middleware
{
    public interface ITokenManager
    {
        Task DeactivateCurrentAsync();
        JwtSecurityToken GenerateToken(User user);
        Task<bool> IsActiveAsync(string token);
        Task<bool> IsCurrentActiveToken();
    }
}