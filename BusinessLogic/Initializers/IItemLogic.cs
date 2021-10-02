using System;
using System.Collections.Generic;
using Models.DataModels.ItemModels;
using Services.DTOs;

namespace BusinessLogic.Initializers
{
    public interface IItemLogic
    {
        IEnumerable<ItemCategory> GetRootCategories(int? rootId);
        IEnumerable<ItemResultDTO> GetItemsIn(int? categoryId, int? carModelId);
        IEnumerable<CarModelResultDTO> GetCarModels(DateTime? cacheTime);
        byte[] GetPictureOfCarModel(int carModelId);
        ItemPictureResultDTO GetPicturesOfItem(int itemId, DateTime? cacheTime);
        void MakeAPurchase(int UserId, IEnumerable<int> itemIds);
    }
}