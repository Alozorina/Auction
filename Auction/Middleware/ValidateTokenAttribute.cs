using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Middleware
{
    public class ValidateTokenAttribute : ServiceFilterAttribute
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
            var isValid = _tokenManager.IsActive(context.HttpContext.Request.Headers["Authorization"]);
            if (!isValid)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
