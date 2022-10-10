using DAL.Entities;

namespace Auction.Middleware
{
    public interface ITokenManager
    {
        bool AddTokenToCache(string token);
        string GetCurrentToken();
        string GetStringToken(User user);
        void InvalidateCurrentToken();
        void InvalidateToken(string token);
        bool IsActive(string token);
        bool IsCurrentActive();
    }
}