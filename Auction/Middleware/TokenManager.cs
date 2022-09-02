using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Middleware
{
    public class TokenManager : ITokenManager
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _validTokens;

        public TokenManager(IConfiguration config,
                IHttpContextAccessor httpContextAccessor,
                IMemoryCache memoryCache)
        {
            _configuration = config;
            _httpContextAccessor = httpContextAccessor;
            _validTokens = memoryCache;
        }

        protected JwtSecurityToken GenerateToken(User user)
        {
            //create claims details based on the user information
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("Email", user.Email),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name.ToString()),
                        new Claim("Role", user.Role.Name.ToString())
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMonths(2),
                signingCredentials: signIn);

            return token;
        }

        public string GetStringToken(User user)
        {
            var token = new JwtSecurityTokenHandler().WriteToken(GenerateToken(user));
            AddTokenToCache(token);
            return token;
        }

        public void InvalidateToken(string token) => _validTokens.Remove(token);

        public void InvalidateCurrentToken() => InvalidateToken(GetCurrentToken());

        public bool AddTokenToCache(string token) => _validTokens.Set(token, true);

        public bool IsActive(string token)
        {
            var valid = _validTokens.Get(token);
            return valid != null;
        }

        public bool IsCurrentActive() => IsActive(GetCurrentToken());

        public string GetCurrentToken()
        {
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty
                ? null
                : authorizationHeader.Single().Split(" ").Last();
        }
    }
}
