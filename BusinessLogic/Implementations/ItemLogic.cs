using System;
using System.Collections.Generic;
using BusinessLogic.Initializers;
using Models.DataModels.ItemModels;
using Services.DTOs;

namespace BusinessLogic.Implementations
{
    public class ItemLogic : IItemLogic
    {
        public IEnumerable<ItemCategory> GetRootCategories(int? rootId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemResultDTO> GetItemsIn(int? categoryId, int? carModelId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CarModelResultDTO> GetCarModels()
        {
            throw new NotImplementedException();
        }

        public byte[] GetCarModelIcon(int carModelId, DateTime? cacheTime)
        {
            throw new NotImplementedException();
        }

        public byte[] GetItemIcon(int itemId, DateTime? cacheTime)
        {
            throw new NotImplementedException();
        }

        public byte[] GetPictureOfItem(int itemId, int index, DateTime? cacheTime)
        {
            throw new NotImplementedException();
        }

        public bool MakeAPurchase(int UserId, IEnumerable<ItemPurchaseRequest> itemIds)
        {
            throw new NotImplementedException();
        }
    }
}