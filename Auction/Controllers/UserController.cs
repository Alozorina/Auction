using AutoMapper;
using BLL;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(ILogger<UserController> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = await _unitOfWork.UserRepository.GetAllWithDetailsAsync();
            _logger.Log(LogLevel.Debug, $"Returned {users.ToList().Count} accounts from database.");
            return Ok(users);
        }

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

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] UserRegistrationModel value)
        {
            if (ModelState.IsValid)
            {
                var isEmailExists = await _unitOfWork.UserRepository.IsEmailExists(value.Email);
                if (!isEmailExists)
                {
                    User user;
                    try
                    {
                        user = _mapper.Map<User>(value);
                        user.RoleId = 1;
                        await _unitOfWork.UserRepository.AddAsync(user);
                        await _unitOfWork.SaveAsync();
                    }
                    catch (AuctionException ex)
                    {
                        return BadRequest(ex.Message);
                    }
                    return CreatedAtAction(nameof(GetById), new { user.Id }, user);
                }
                return BadRequest("This email already exists");
            }
            return BadRequest();
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int Id, [FromBody] User value)
        {
            if (Id != value.Id)
                return BadRequest();
            try
            {
                await _unitOfWork.UserRepository.UpdateAsync(value);
                await _unitOfWork.SaveAsync();
            }
            catch (AuctionException ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetById), new { value.Id }, value);
        }

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

    }
}
