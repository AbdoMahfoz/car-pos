using System;
using System.Collections.Generic;
using Services.DTOs;

namespace BusinessLogic.Interfaces
{
    public interface IItemLogic
    {
        IEnumerable<ItemCategoryResultDTO> GetRootCategories();
        IEnumerable<ItemResultDTO> GetItemsIn(string query, int? categoryId, int? carModelId);
        IEnumerable<CarModelResultDTO> GetCarModels();
        byte[] GetCarModelIcon(int carModelId, DateTime? cacheTime);
        byte[] GetItemIcon(int itemId, DateTime? cacheTime);
        byte[] GetPictureOfItem(int itemId, int index, DateTime? cacheTime);
        bool MakeAPurchase(int UserId, IEnumerable<ItemPurchaseRequest> itemIds);
    }
}