using System;
using System.Collections.Generic;
using Models.DataModels.ItemModels;
using Services.DTOs;

namespace BusinessLogic.Initializers
{
    public interface IItemLogic
    {
        IEnumerable<ItemCategoryResultDTO> GetRootCategories();
        IEnumerable<ItemResultDTO> GetItemsIn(int? categoryId, int? carModelId);
        IEnumerable<CarModelResultDTO> GetCarModels();
        byte[] GetCarModelIcon(int carModelId, DateTime? cacheTime);
        byte[] GetItemIcon(int itemId, DateTime? cacheTime);
        byte[] GetPictureOfItem(int itemId, int index, DateTime? cacheTime);
        bool MakeAPurchase(int UserId, IEnumerable<ItemPurchaseRequest> itemIds);
    }
}