using BLL.Models;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public TokenController(IConfiguration config, IUnitOfWork unitOfWork)
        {
            _configuration = config;
            _unitOfWork = unitOfWork;
        }


        [HttpPost]
        public async Task<IActionResult> Post(UserLoginModel _userLoginData)
        {
            if (_userLoginData != null && _userLoginData.Email != null && _userLoginData.Password != null)
            {
                var userExists = await _unitOfWork.UserRepository
                    .FirstOrDefaultAsync(u => u.Email == _userLoginData.Email && u.Password == _userLoginData.Password);

                if (userExists == null)
                    return Unauthorized("Invalid credentials");

                //create claims details based on the userExists information
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", userExists.Id.ToString()),
                        new Claim("Email", userExists.Email),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, userExists.Role.Name.ToString())
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
