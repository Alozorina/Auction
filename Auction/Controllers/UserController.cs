using Auction.Business.Services;
using Auction.Middleware;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        private readonly ITokenManager _tokenManager;

        public UserController(IUserService userService, ILogger<UserController> logger, ITokenManager tokenManager)
        {
            _userService = userService;
            _logger = logger;
            _tokenManager = tokenManager;
        }

        [ValidateToken]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPulicInfo>>> Get()
        {
            try
            {
                var users = await _userService.GetPublicInfoAsync();
                return users != null ? Ok(users) : NotFound("No users found");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"{ex.Message}");
                return NotFound();
            }
        }

        [ValidateToken]
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            try
            {
                var user = await _userService.GetUserWithDetailsByIdAsync(id);
                return user != null ? Ok(user) : NotFound($"Account with id: {id}, hasn't been found in db.");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"{ex.Message}");
                return NotFound();
            }
        }

        [ValidateToken]
        [Authorize(Roles = "Admin, User")]
        [HttpGet("profile")]
        public async Task<ActionResult<UserPersonalInfoModel>> GetCurrentUserPersonalInfo()
        {
            var currentUser = await GetUser();
            if (currentUser == null)
            {
                _logger.Log(LogLevel.Error, "Authorization access error");
                return Unauthorized("Authorization access error");
            }
            return Ok(_userService.MapToUserPersonalInfoFromUser(currentUser));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserRegistrationModel value)
        {
            if (ModelState.IsValid && !_tokenManager.IsCurrentActive())
            {
                try
                {
                    var user = await _userService.AddAsync(value);
                    return Ok(_tokenManager.GetStringToken(user));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModel userLoginData)
        {
            if (userLoginData == null || string.IsNullOrEmpty(userLoginData.Email) || string.IsNullOrEmpty(userLoginData.Password))
                return BadRequest();

            try
            {
                var userExists = await _userService.LoginAsync(userLoginData.Email, userLoginData.Password);
                return Ok(_tokenManager.GetStringToken(userExists));
            }
            catch
            {
                return Unauthorized("Invalid credentials");
            }
        }

        [ValidateToken]
        [Authorize(Roles = "Admin, User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            _tokenManager.InvalidateCurrentToken();
            HttpContext.User = null;
            return Ok();
        }

        [ValidateToken]
        [Authorize(Roles = "Admin, User")]
        [HttpPut("profile/edit")]
        public async Task<ActionResult> UpdateCurrentUserPersonalInfo([FromBody] UserPersonalInfoModel updateModel)
        {
            var currentUser = await GetUser();
            try
            {
                await _userService.UpdatePersonalInfo(currentUser.Id, updateModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetCurrentUserPersonalInfo), updateModel);
        }

        [ValidateToken]
        [Authorize(Roles = "Admin, User")]
        [HttpPut("profile/password")]
        public async Task<ActionResult> UpdatePassword([FromBody] UserPassword updateModel)
        {
            var currentUser = await GetUser();
            try
            {
                await _userService.UpdatePassword(currentUser, updateModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Updated successfully");
        }

        [ValidateToken]
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/edit")]
        public async Task<ActionResult> UpdatePersonalInfoById(int id, [FromBody] UserPersonalInfoModel updateModel)
        {
            try
            {
                await _userService.UpdatePersonalInfo(id, updateModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetById), new { id }, updateModel);
        }

        [ValidateToken]
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/creds")]
        public async Task<ActionResult> UpdatePasswordById(int id, [FromBody] UserPassword updateModel)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                await _userService.UpdatePassword(user, updateModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Updated successfully");
        }

        [ValidateToken]
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/role")]
        public async Task<ActionResult> UpdateRoleById(int id, [FromBody] int roleId)
        {
            try
            {
                await _userService.UpdateRoleAsync(id, roleId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Updated successfully");
        }

        [ValidateToken]
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteByIdAsync(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            return Ok($"User with Id: {id} deleted successfully");
        }

        private async Task<User> GetUser()
        {
            return await _userService
                .GetUserByIdAsync(
                    Convert.ToInt32(_tokenManager.GetUserIdFromToken()));
        }
    }
}
