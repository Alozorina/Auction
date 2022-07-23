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

        [HttpGet("search={searchString}")]
        public async Task<ActionResult<IEnumerable<ItemPublicInfo>>> Index(string searchString)
        {
            var items = await _unitOfWork.ItemRepository.GetAllPublicWithDetailsAsync(_mapper);
            IEnumerable<ItemPublicInfo> foundBySearchString = new List<ItemPublicInfo>();
            if (!String.IsNullOrEmpty(searchString))
            {
                foundBySearchString = items.Where(i =>
                   i.Name.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
                || i.CreatedBy.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
                || i.ItemCategoryNames.Contains(i.ItemCategoryNames
                                                .FirstOrDefault(ic => ic
                                                    .Contains(searchString, StringComparison.InvariantCultureIgnoreCase))))
                                            .ToList();
            }
            _logger.Log(LogLevel.Debug, $"Returned {items.Count} items from database.");
            return Ok(foundBySearchString);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("private")]
        public async Task<ActionResult<IEnumerable<Item>>> GetAll()
        {
            var items = await _unitOfWork.ItemRepository.GetAllWithDetailsAsync();
            return Ok(items);
        }

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

        [Authorize(Roles = "Admin, User")]
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

        [HttpPost("[action]")]
        public async Task<IActionResult> UploadImages(List<IFormFile> files)
        {
            if (files.Count == 0)
                return BadRequest();

            List<string> imageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".PNG" };
            string path = Path.Combine(_webHostEnvironment.ContentRootPath, "StaticFiles", "images");
            string uploadedFileNames = "";
            foreach (var file in files)
            {
                string extension = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                if (imageExtensions.Contains(extension.ToUpperInvariant()))
                {
                    string uniqueFileName = "veilingID" + file.FileName;
                    uploadedFileNames += uniqueFileName + "; ";
                    string filePath = Path.Combine(path, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
            return Ok(new { uploaded = uploadedFileNames });
        }

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
