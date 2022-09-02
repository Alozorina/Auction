using DAL.Entities;

namespace Auction.Middleware
{
    public interface ITokenManager
    {
        bool AddTokenToCache(string token);
        string GetCurrentToken();
        string GetStringToken(User user);
        bool InvalidateCurrentToken();
        bool InvalidateToken(string token);
        bool IsActive(string token);
        bool IsCurrentActive();
    }
}