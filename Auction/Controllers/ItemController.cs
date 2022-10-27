using Auction.Business.Services;
using Auction.Middleware;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IItemService _itemService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ItemController(ILogger<ItemController> logger, IItemService itemService, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _itemService = itemService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemPublicInfo>>> GetPublicSortedByStartDate()
        {
            var items = await _itemService.GetAllPublicWithDetailsAsync();
            _logger.Log(LogLevel.Debug, $"Returned {items.Count} items from database.");
            return Ok(items);
        }

        //TODO rewrite
        [ValidateToken]
        [Authorize(Roles = "Admin, User")]
        [HttpGet("lots/user={id}")]
        public async Task<ActionResult<IEnumerable<ItemPublicInfo>>> GetLotsByUserId(int id)
        {
            try
            {
                var lots = await _itemService.GetLotsByUserIdAsync(id);
                _logger.Log(LogLevel.Debug, $"Returned {lots.Count} lots from database.");
                return Ok(lots);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ValidateToken]
        [Authorize(Roles = "Admin, User")]
        [HttpGet("purchases/user={id}")]
        public async Task<ActionResult<IEnumerable<ItemPublicInfo>>> GetPurchasesByUserId(int id)
        {
            try
            {
                var purchases = await _itemService.GetPurchasesByUserIdAsync(id);
                _logger.Log(LogLevel.Debug, $"Returned {purchases.Count} lots from database.");
                return Ok(purchases);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search={searchParams}")]
        public async Task<ActionResult<IEnumerable<ItemPublicInfo>>> Search(string searchParams)
        {
            try
            {
                var items = await _itemService.SearchItemsAsync(searchParams);
                _logger.Log(LogLevel.Debug, $"Returned {items.Count} items from database.");
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ValidateToken]
        [Authorize(Roles = "Admin")]
        [HttpGet("private")]
        public async Task<ActionResult<IEnumerable<Item>>> GetAll()
        {
            try
            {
                var items = await _itemService.GetAllItemsWithDetailsAsync();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [ValidateToken]
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetById(int id)
        {
            try
            {
                var item = await _itemService.GetItemByIdAsync(id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Item with id: {id}, hasn't been found in db.");
                return NotFound(ex.Message);
            }
        }

        [ValidateToken]
        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public async Task<ActionResult> Add([FromForm] ItemCreateNewEntity value)
        {
            try
            {
                var contentRootPath = _webHostEnvironment.ContentRootPath;
                var item = await _itemService.AddItemAsync(contentRootPath, value);
                return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ValidateToken]
        [Authorize(Roles = "Admin, User")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBid(int id, [FromBody] ItemUpdateBid data)
        {
            try
            {
                var item = await _itemService.UpdateBidByIdAsync(id, data);
                return CreatedAtAction(nameof(GetById), new { item.Id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ValidateToken]
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/edit")]
        public async Task<ActionResult> Update(int id, [FromBody] ItemPublicInfo value)
        {
            if (id != value.Id)
                return BadRequest("Wrong Id");
            try
            {
                var item = await _itemService.UpdateItemAsync(value);
                return CreatedAtAction(nameof(GetById), new { value.Id }, value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ValidateToken]
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _itemService.DeleteItemAsync(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return Ok($"Item with Id: {id} deleted successfully");
        }
    }
}
