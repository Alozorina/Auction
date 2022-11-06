using Data.Entities;

namespace Web.Middleware
{
    public interface ITokenManager
    {
        bool AddTokenToCache(string token);
        string GetCurrentToken();
        string GetStringToken(User user);
        string GetUserIdFromToken();
        void InvalidateCurrentToken();
        void InvalidateToken(string token);
        bool IsActive(string token);
        bool IsCurrentActive();
    }
}