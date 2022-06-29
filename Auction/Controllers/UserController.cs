using Auction.Middleware;
using AutoMapper;
using BLL;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenManager _tokenManager;

        public UserController(ILogger<UserController> logger, IMapper mapper, IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _tokenManager = tokenManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = await _unitOfWork.UserRepository.GetAllWithDetailsAsync();
            _logger.Log(LogLevel.Debug, $"Returned {users.ToList().Count} accounts from database.");
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

            if (user == null)
            {
                _logger.Log(LogLevel.Error, $"Account with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            return Ok(user);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("profile")]
        public async Task<ActionResult<UserPersonalInfoModel>> GetCurrentUserPersonalInfo()
        {
            var currentUser = await GetUser();

            if (currentUser == null)
            {
                _logger.Log(LogLevel.Error, "Authorization access error");
                return NotFound();
            }
            var personalInfo = _mapper.Map<UserPersonalInfoModel>(currentUser);

            return Ok(personalInfo);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserRegistrationModel value)
        {
            var token = await GetTokenFromContext();
            if (ModelState.IsValid && token == null)
            {
                var isEmailExists = await _unitOfWork.UserRepository.IsEmailExists(value.Email);
                if (!isEmailExists)
                {
                    User user;
                    try
                    {
                        user = _mapper.Map<User>(value);
                        await _unitOfWork.UserRepository.AddAsync(user);
                        await _unitOfWork.SaveAsync();
                    }
                    catch (AuctionException ex)
                    {
                        return BadRequest(ex.Message);
                    }
                    return Ok(new JwtSecurityTokenHandler().WriteToken(_tokenManager.GenerateToken(user)));
                }
                return BadRequest("This email is being used by another user");
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModel _userLoginData)
        {
            if (_userLoginData != null && _userLoginData.Email != null && _userLoginData.Password != null)
            {
                var userExists = await _unitOfWork.UserRepository
                    .FirstOrDefaultAsync(u => u.Email == _userLoginData.Email && u.Password == _userLoginData.Password);

                if (userExists == null)
                    return Unauthorized("Invalid credentials");

                var token = _tokenManager.GenerateToken(userExists);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin, User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _tokenManager.DeactivateCurrentAsync();
            return NoContent();
        }


        [Authorize(Roles = "Admin, User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("profile/edit")]
        public async Task<ActionResult> UpdateCurrentUserPersonalInfo([FromBody] UserPersonalInfoModel updateModel)
        {
            var currentUser = await GetUser();
            try
            {
                var update = _mapper.Map(updateModel, currentUser);

                await _unitOfWork.UserRepository.UpdateAsync(update);
                await _unitOfWork.SaveAsync();
            }
            catch (AuctionException ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetById), new { currentUser.Id }, updateModel);
        }

        [Authorize(Roles = "Admin, User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("profile/creds")]
        public async Task<ActionResult> UpdateLoginPassword([FromBody] UserCreds updateModel)
        {
            var currentUser = await GetUser();
            try
            {
                var isEmailExists = await _unitOfWork.UserRepository.IsEmailExists(updateModel.Email);
                if (isEmailExists && currentUser.Email != updateModel.Email)
                    return BadRequest("This email is being used by another user");

                var update = _mapper.Map(updateModel, currentUser);

                await _unitOfWork.UserRepository.UpdateAsync(update);
                await _unitOfWork.SaveAsync();
            }
            catch (AuctionException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Updated successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("edit/{id}")]
        public async Task<ActionResult> UpdatePersonalInfoById(int id, [FromBody] UserPersonalInfoModel updateModel)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            try
            {
                var update = _mapper.Map(updateModel, user);

                await _unitOfWork.UserRepository.UpdateAsync(update);
                await _unitOfWork.SaveAsync();
            }
            catch (AuctionException ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetById), new { id }, updateModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("creds/{id}")]
        public async Task<ActionResult> UpdateLoginPasswordById(int id, [FromBody] UserCreds updateModel)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            try
            {
                var isEmailExists = await _unitOfWork.UserRepository.IsEmailExists(updateModel.Email);
                if (isEmailExists && user.Email != updateModel.Email)
                    return BadRequest("This email is being used by another user");

                var update = _mapper.Map(updateModel, user);

                await _unitOfWork.UserRepository.UpdateAsync(update);
                await _unitOfWork.SaveAsync();
            }
            catch (AuctionException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Updated successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("creds/{id}")]
        public async Task<ActionResult> UpdateRoleById(int id, [FromBody] int roleId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            try
            {
                user.RoleId = roleId;
                await _unitOfWork.UserRepository.UpdateAsync(user);
                await _unitOfWork.SaveAsync();
            }
            catch (AuctionException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Updated successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

            if (user == null)
                return BadRequest();

            await _unitOfWork.UserRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();

            return Ok(user);
        }


        private async Task<User> GetUser()
        {
            return await _unitOfWork.UserRepository
                .GetByIdAsync(
                    Convert.ToInt32(
                        HttpContext.User.FindFirst("Id").Value));
        }

        async Task<string> GetTokenFromContext() => await HttpContext.GetTokenAsync("access_token");

        //TODO: delete this query
        [HttpGet("token")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<string>> Gettoken()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var user = HttpContext.User.FindFirst("Id").Value;
            return Ok(token + "  " + user);
        }

    }
}
