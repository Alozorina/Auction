using Auction.Middleware;
using AutoMapper;
using BLL;
using BLL.Extensions;
using BLL.Models;
using BLL.Validation;
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
        public async Task<ActionResult<IEnumerable<UserPulicInfo>>> Get()
        {
            var users = await _unitOfWork.UserRepository.GetAllWithDetailsAsync();
            List<UserPulicInfo> usersPulicInfo = new List<UserPulicInfo>();
            foreach (var user in users)
            {
                usersPulicInfo.Add(_mapper.Map<UserPulicInfo>(user));
            }
            _logger.Log(LogLevel.Debug, $"Returned {usersPulicInfo.Count} accounts from database.");
            return Ok(usersPulicInfo);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdWithDetailsAsync(id);

            if (user == null)
            {
                _logger.Log(LogLevel.Error, $"Account with id: {id}, hasn't been found in db.");
                return Unauthorized();
            }

            return Ok(user);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("profile")]
        public async Task<ActionResult<UserPersonalInfoModel>> GetCurrentUserPersonalInfo()
        {
            if (!_tokenManager.IsCurrentActive())
            {
                return Unauthorized();
            }
            var currentUser = await GetUser();
            if (currentUser == null)
            {
                _logger.Log(LogLevel.Error, "Authorization access error");
                return NotFound();
            }

            var personalInfo = _mapper.Map<User, UserPersonalInfoModel>(currentUser);
            return Ok(personalInfo);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserRegistrationModel value)
        {
            var token = _tokenManager.GetCurrentToken();
            if (ModelState.IsValid && token == null)
            {
                User user;
                const int USER_ROLE_ID = 1;
                Role role = await _unitOfWork.RoleRepository.FirstOrDefaultAsync(r => r.Id == USER_ROLE_ID);
                try
                {
                    user = _mapper.Map<User>(value);
                    user.RoleId = USER_ROLE_ID;
                    user.Role = role;
                    await _unitOfWork.UserRepository.AddAsync(user);
                    await _unitOfWork.SaveAsync();
                }
                catch (AuctionException ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok(_tokenManager.GetStringToken(user));
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

                var token = _tokenManager.GetStringToken(userExists);
                return Ok(token);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin, User", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var isSuccess = _tokenManager.InvalidateCurrentToken();
            HttpContext.User = null;
            return isSuccess ? Ok("Logout successfull") : Ok("Something went wrong");
        }


        [Authorize(Roles = "Admin, User")]
        [HttpPut("profile/edit")]
        public async Task<ActionResult> UpdateCurrentUserPersonalInfo([FromBody] UserPersonalInfoModel updateModel)
        {
            var currentUser = await GetUser();
            try
            {
                var update = _mapper.Map(updateModel, currentUser);
                update.BirthDate = DataConverter.ConvertDateFromClient(updateModel.BirthDate);

                await _unitOfWork.UserRepository.UpdateAsync(update);
                await _unitOfWork.SaveAsync();
            }
            catch (AuctionException ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetCurrentUserPersonalInfo), updateModel);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("profile/password")]
        public async Task<ActionResult> UpdatePassword([FromBody] UserPassword updateModel)
        {
            var currentUser = await GetUser();
            try
            {
                bool isPasswordMatches = UserValidation.IsClientPasswordMatches(updateModel.OldPassword, currentUser.Password);
                if (!isPasswordMatches)
                    throw new AuctionException("Wrong Password");

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
        [HttpPut("{id}/edit")]
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
        [HttpPut("{id}/creds")]
        public async Task<ActionResult> UpdatePasswordById(int id, [FromBody] UserPassword updateModel)
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
            return Ok("Updated successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/role")]
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
    }
}
