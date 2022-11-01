using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.Business.Services
{
    public interface IItemService
    {
        Task<Item> AddItemAsync(string contentRootPath, ItemCreateNewEntity value);
        Task DeleteItemAsync(int id);
        Task<IEnumerable<Item>> GetAllItemsWithDetailsAsync();
        Task<List<ItemPublicInfo>> GetAllPublicWithDetailsAsync();
        Task<Item> GetItemByIdAsync(int id);
        Task<Item> GetItemWithDetailsByIdAsync(int id);
        Task<List<ItemPublicInfo>> GetLotsByUserIdAsync(int id);
        Task<List<ItemPublicInfo>> GetPurchasesByUserIdAsync(int id);
        Task<Item> UpdateBidByIdAsync(int id, ItemUpdateBid data);
        Task<Item> UpdateItemAsync(ItemPublicInfo value);
        Task<List<string>> UploadImages(string contentRootPath, List<IFormFile> files);
        Task<List<ItemPublicInfo>> SearchItemsAsync(string searchParams);
    }
}