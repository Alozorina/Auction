using Auction.Middleware;
using AutoMapper;
using BLL;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ItemController(ILogger<ItemController> logger, IMapper mapper, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemPublicInfo>>> GetPublicSortedByStartDate()
        {
            var items = await _unitOfWork.ItemRepository.GetAllPublicWithDetailsAsync(_mapper);
            _logger.Log(LogLevel.Debug, $"Returned {items.Count} items from database.");
            return Ok(items);
        }

        [ValidateToken]
        [Authorize(Roles = "Admin, User")]
        [HttpGet("lots/user={id}")]
        public async Task<ActionResult<IEnumerable<ItemPublicInfo>>> GetLotsByUserId(int id)
        {
            var items = await _unitOfWork.ItemRepository.GetAllPublicWithDetailsAsync(_mapper);
            var lots = items.Where(i => i.OwnerId == id).OrderBy(i => i.StartSaleDate).ToList();
            _logger.Log(LogLevel.Debug, $"Returned {lots.Count} lots from database.");
            return Ok(lots);
        }

        [ValidateToken]
        [Authorize(Roles = "Admin, User")]
        [HttpGet("purchases/user={id}")]
        public async Task<ActionResult<IEnumerable<ItemPublicInfo>>> GetPurchasesByUserId(int id)
        {
            var items = await _unitOfWork.ItemRepository.GetAllPublicWithDetailsAsync(_mapper);
            var purchases = items.Where(i => i.BuyerId == id).OrderBy(i => i.StartSaleDate).ToList();
            _logger.Log(LogLevel.Debug, $"Returned {purchases.Count} purchases from database.");
            return Ok(purchases);
        }

        [HttpGet("search={searchParams}")]
        public async Task<ActionResult<IEnumerable<ItemPublicInfo>>> Search(string searchParams)
        {
            var items = await _unitOfWork.ItemRepository.GetAllPublicWithDetailsAsync(_mapper);
            IEnumerable<ItemPublicInfo> foundBySearchParams = new List<ItemPublicInfo>();
            if (!String.IsNullOrEmpty(searchParams))
            {
                foundBySearchParams = items
                    .Where(i =>
                       i.Name.Contains(searchParams, StringComparison.CurrentCultureIgnoreCase)
                    || i.CreatedBy.Contains(searchParams, StringComparison.CurrentCultureIgnoreCase)
                    || i.ItemCategories.Contains(i.ItemCategories
                                                    .FirstOrDefault(ic => ic.Category.Name
                                                            .Contains(searchParams, StringComparison.CurrentCultureIgnoreCase))))
                    .ToList();
            }
            _logger.Log(LogLevel.Debug, $"Returned {items.Count} items from database.");
            return Ok(foundBySearchParams);
        }

        [ValidateToken]
        [Authorize(Roles = "Admin")]
        [HttpGet("private")]
        public async Task<ActionResult<IEnumerable<Item>>> GetAll()
        {
            var items = await _unitOfWork.ItemRepository.GetAllWithDetailsAsync();
            return Ok(items);
        }

        [ValidateToken]
        [Authorize(Roles = "Admin")]
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

        [ValidateToken]
        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public async Task<ActionResult> Add([FromForm] ItemCreateNewEntity value)
        {
            try
            {
                List<string> uploadedFileNames = await UploadImages(value.ItemFormFilePhotos);
                Item item = _mapper.Map<Item>(value);
                item.StatusId = 1;
                item.ItemPhotos = new List<ItemPhoto>();
                foreach (var uploadedFileName in uploadedFileNames)
                {
                    var photo = new ItemPhoto()
                    {
                        ItemId = item.Id,
                        Path = uploadedFileName,
                    };
                    item.ItemPhotos.Add(photo);
                }

                await _unitOfWork.ItemRepository.AddAsync(item);
                await _unitOfWork.SaveAsync();
                return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
            }
            catch (AuctionException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        async Task<List<string>> UploadImages(List<IFormFile> files)
        {
            List<string> imageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".PNG" };
            string path = Path.Combine(_webHostEnvironment.ContentRootPath, "StaticFiles", "images");
            List<string> uploadedFileNames = new List<string>();
            foreach (var file in files)
            {
                string extension = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                if (imageExtensions.Contains(extension.ToUpperInvariant()))
                {
                    string uniqueFileName = Guid.NewGuid().GetHashCode().ToString() + "_" + file.FileName;
                    uploadedFileNames.Add(uniqueFileName);
                    string filePath = Path.Combine(path, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
            return uploadedFileNames;
        }

        [ValidateToken]
        [Authorize(Roles = "Admin, User")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBid(int id, [FromBody] ItemUpdateBid data)
        {
            if (data == null)
                return BadRequest();

            var item = await _unitOfWork.ItemRepository.GetByIdAsync(id);
            try
            {
                var mapped = _mapper.Map(data, item);
                await _unitOfWork.ItemRepository.UpdateAsync(mapped);
                await _unitOfWork.SaveAsync();
            }
            catch (AuctionException ex)
            {
                return BadRequest(ex.Message);
            }
            return CreatedAtAction(nameof(GetById), new { item.Id }, item);
        }

        [ValidateToken]
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/edit")]
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

        [ValidateToken]
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
