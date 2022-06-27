using BLL;
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
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ItemController(ILogger<ItemController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> Get()
        {
            var items = await _unitOfWork.ItemRepository.GetAllWithDetailsAsync();
            _logger.Log(LogLevel.Debug, $"Returned {items.ToList().Count} accounts from database.");
            return Ok(items);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetById(int id)
        {
            var item = await _unitOfWork.ItemRepository.GetByIdAsync(id);

            if (item == null)
            {
                _logger.Log(LogLevel.Error, $"Item with id: {id}, hasn't been found in db.");
                return NotFound();
            }

            return Ok(item);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] Item value)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork.ItemRepository.AddAsync(value);
                    await _unitOfWork.SaveAsync();
                }
                catch (AuctionException ex)
                {
                    return BadRequest(ex.Message);
                }
                return CreatedAtAction(nameof(GetById), new { value.Id }, value);
            }
            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int Id, [FromBody] Item value)
        {
            if (Id != value.Id)
                return BadRequest();
            try
            {
                await _unitOfWork.ItemRepository.UpdateAsync(value);
                await _unitOfWork.SaveAsync();
            }
            catch (AuctionException ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetById), new { value.Id }, value);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var item = await _unitOfWork.ItemRepository.GetByIdAsync(id);
            try
            {
                if (item == null)
                    return BadRequest();

                await _unitOfWork.ItemRepository.DeleteByIdAsync(id);
                await _unitOfWork.SaveAsync();
            }
            catch (AuctionException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
