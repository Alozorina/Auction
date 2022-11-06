using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Middleware
{
    public class ValidateTokenAttribute : TypeFilterAttribute
    {
        public ValidateTokenAttribute() : base(typeof(TokenComparer)) { }
    }
    public class TokenComparer : IAuthorizationFilter
    {
        private readonly ITokenManager _tokenManager;

        public TokenComparer(ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!_tokenManager.IsCurrentActive())
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
