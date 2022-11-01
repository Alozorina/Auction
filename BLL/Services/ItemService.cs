using AutoMapper;
using BLL.Models;
using BLL.Validation;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Business.Services
{
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all items with details from the database
        /// </summary>
        /// <remarks> 
        /// Uses no tracking behavior
        /// </remarks>
        /// <returns>
        /// Async Task. Task result contains IEnumerable<Item> that contains elements from the input sequence
        /// </returns>
        public async Task<IEnumerable<Item>> GetAllItemsWithDetailsAsync() => await _unitOfWork.ItemRepository.GetAllWithDetails();

        /// <summary>
        /// Gets info about Item with all details
        /// </summary>
        /// <remarks> 
        /// Navigation properties included
        /// </remarks>
        /// <returns>
        /// Async Task. Task result contains the single Item with the given ID, or throws an Argument Exception if Item is null
        /// </returns>
        public async Task<Item> GetItemWithDetailsByIdAsync(int id)
        {
            var item = await _unitOfWork.ItemRepository.GetByIdWithDetailsAsync(id);
            ItemValidation.ThrowArgumentExceptionIfItemWasntFound(item);

            return item;
        }

        /// <summary>
        /// Gets info about Item without details
        /// </summary>
        /// <remarks> 
        /// Navigation properties will be null
        /// </remarks>
        /// <returns>
        /// Async Task. Task result contains the single Item with the given ID, or throws an Argument Exception if Item is null
        /// </returns>
        public async Task<Item> GetItemByIdAsync(int id)
        {
            var item = await _unitOfWork.ItemRepository.GetByIdAsync(id);
            ItemValidation.ThrowArgumentExceptionIfItemWasntFound(item);

            return item;
        }

        /// <summary>
        /// Gets all items from the database, maps them to <ItemPublicInfo> and makes the nested navigation properties null
        /// </summary>
        /// <returns>
        /// Async Task. Task result contains List<ItemPublicInfo> that contains elements from the input sequence
        /// </returns>
        public async Task<List<ItemPublicInfo>> GetAllPublicWithDetailsAsync()
        {
            return await _unitOfWork.ItemRepository.GetAllPublicDetails()
                .OrderBy(i => i.StartSaleDate)
                .AsNoTracking()
                .Select(item => _mapper.Map<ItemPublicInfo>(item))
                .ToListAsync();
        }

        /// <summary>
        /// Gets items with the given Owner Id from the database, maps them to <ItemPublicInfo> 
        /// and makes the nested navigation properties null
        /// </summary>
        /// <returns>
        /// Async Task. Task result contains List<ItemPublicInfo> that contains elements from the input sequence 
        /// and sorted by Start Sale Date 
        /// </returns>
        public async Task<List<ItemPublicInfo>> GetLotsByUserIdAsync(int id)
        {
            return await _unitOfWork.ItemRepository.GetAllPublicDetails()
                .Where(i => i.OwnerId == id)
                .OrderBy(i => i.StartSaleDate)
                .Select(item => _mapper.Map<ItemPublicInfo>(item))
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Gets items with the given Buyer Id from the database, maps them to <ItemPublicInfo> 
        /// and makes the nested navigation properties null
        /// </summary>
        /// <returns>
        /// Async Task. Task result contains List<ItemPublicInfo> that contains elements from the input sequence 
        /// and sorted by Start Sale Date 
        /// </returns>
        public async Task<List<ItemPublicInfo>> GetPurchasesByUserIdAsync(int id)
        {
            return await _unitOfWork.ItemRepository.GetAllPublicDetails()
                .Where(i => i.BuyerId == id)
                .OrderBy(i => i.StartSaleDate)
                .Select(item => _mapper.Map<ItemPublicInfo>(item))
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Adds new data to the beginning of the given images name: "veiling_" and unique hash code.
        /// Then uploads to given content root path with selected directory
        /// </summary>
        /// <returns>
        /// Async Task. Task result contains List<string> that contains all names of uploaded images
        /// </returns>
        public async Task<List<string>> UploadImages(string contentRootPath, List<IFormFile> files)
        {
            List<string> imageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".PNG" };
            string path = Path.Combine(contentRootPath, "StaticFiles", "images");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            List<string> uploadedFileNames = new List<string>();
            foreach (var file in files)
            {
                string extension = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                if (imageExtensions.Contains(extension.ToUpperInvariant()))
                {
                    string uniqueFileName = "veiling_" + Guid.NewGuid().GetHashCode().ToString() + "_" + file.FileName;
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

        /// <summary>
        /// Adds new Item to database
        /// </summary>
        /// <returns>
        /// Async Task. Task result contains Item that was added to database
        /// </returns>
        public async Task<Item> AddItemAsync(string contentRootPath, ItemCreateNewEntity value)
        {
            List<string> uploadedFileNames = await UploadImages(contentRootPath, value.ItemFormFilePhotos);
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

            return item;
        }

        /// <summary>
        /// Gets Item with given id and updates Current Bid and Buyer Id properties. 
        /// If Item wasn't found, throws an exception
        /// </summary>
        /// <returns>
        /// Async Task. Task result contains updated Item
        /// </returns>
        public async Task<Item> UpdateBidByIdAsync(int id, ItemUpdateBid data)
        {
            var item = await _unitOfWork.ItemRepository.GetByIdAsync(id);
            ItemValidation.ThrowArgumentExceptionIfItemWasntFound(item);

            item.CurrentBid = data.CurrentBid;
            item.BuyerId = data.BuyerId;
            await _unitOfWork.SaveAsync();
            return item;
        }

        /// <summary>
        /// Gets Item with id from given <ItemPublicInfo> entity, maps it to <Item> and updates it in database. 
        /// If Item wasn't found, throws an exception
        /// </summary>
        /// <returns>
        /// Async Task. Task result contains updated Item
        /// </returns>
        public async Task<Item> UpdateItemAsync(ItemPublicInfo value)
        {
            var mapped = _mapper.Map<Item>(value);
            await _unitOfWork.ItemRepository.UpdateAsync(mapped);
            await _unitOfWork.SaveAsync();
            return mapped;
        }

        /// <summary>
        /// Gets items with the given search parameters from the database, maps them to <ItemPublicInfo> 
        /// and makes the nested navigation properties null
        /// </summary>
        /// <returns>
        /// Async Task. Task result contains List<ItemPublicInfo> that contains elements from the input sequence.
        /// </returns>
        public async Task<List<ItemPublicInfo>> SearchItemsAsync(string searchParams)
        {
            if (string.IsNullOrEmpty(searchParams))
                throw new ArgumentException("Empty search parameter");

            return await _unitOfWork.ItemRepository.GetAllPublicDetails()
                    .SelectMany(i => i.ItemCategories, (Item, ItemCategory) => new { Item, ItemCategory })
                    .Where(ItemAndCategory =>
                      ItemAndCategory.Item.Name.Contains(searchParams, StringComparison.CurrentCultureIgnoreCase)
                    | ItemAndCategory.Item.CreatedBy.Contains(searchParams, StringComparison.CurrentCultureIgnoreCase)
                    | ItemAndCategory.ItemCategory.Category.Name.Contains(searchParams, StringComparison.CurrentCultureIgnoreCase))
                    .Select(ItemAndCategory => _mapper.Map<ItemPublicInfo>(ItemAndCategory.Item))
                    .AsNoTracking()
                    .ToListAsync();
        }

        /// <summary>
        /// Gets an Item by given Id and removes it from the database.
        /// If Item wasn't found, throws an exception
        /// </summary>
        public async Task DeleteItemAsync(int id)
        {
            var item = await GetItemWithDetailsByIdAsync(id);
            _unitOfWork.ItemRepository.Delete(item);
            await _unitOfWork.SaveAsync();
        }
    }
}
