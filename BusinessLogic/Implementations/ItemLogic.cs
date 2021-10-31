using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BusinessLogic.Initializers;
using BusinessLogic.Interfaces;
using Models.DataModels;
using Models.DataModels.ItemModels;
using Repository;
using Repository.ExtendedRepositories;
using Services.DTOs;
using Services.Helpers;

namespace BusinessLogic.Implementations
{
    public class ItemLogic : IItemLogic
    {
        private readonly IItemCategoryRepository ItemCategoryRepository;
        private readonly IItemRepository ItemRepository;
        private readonly IRepository<ItemCarModel> CarModelRepository;
        private readonly IRepository<ItemCarModelIcon> CarModelIconRepository;
        private readonly IItemPictureRepository ItemPictureRepository;
        private readonly IRepository<Receit> ReceitRepository;
        private readonly ISearch Search;
        private static readonly object PurchaseLock = new object();

        public ItemLogic(IItemCategoryRepository itemCategoryRepository, IItemRepository itemRepository,
            IRepository<ItemCarModel> carModelRepository, IRepository<ItemCarModelIcon> carModelIconRepository,
            IItemPictureRepository itemPictureRepository, IRepository<Receit> receitRepository, ISearch search)
        {
            this.ItemCategoryRepository = itemCategoryRepository;
            this.ItemRepository = itemRepository;
            this.CarModelRepository = carModelRepository;
            this.CarModelIconRepository = carModelIconRepository;
            this.ItemPictureRepository = itemPictureRepository;
            this.ReceitRepository = receitRepository;
            this.Search = search;
        }

        private static ItemCategoryResultDTO getRootCategoriesHelper(ItemCategory cur)
        {
            var res = ObjectHelpers.MapTo<ItemCategoryResultDTO>(cur);
            res.Children = cur.ChildrenCategories.ToArray().Select(getRootCategoriesHelper);
            return res;
        }

        private static ItemResultDTO itemHelper(Item item)
        {
            var res = ObjectHelpers.MapTo<ItemResultDTO>(item);
            res.CategoryName = item.Category.Name;
            return res;
        }

        public IEnumerable<ItemCategoryResultDTO> GetRootCategories()
        {
            return ItemCategoryRepository.GetRootCategories().ToArray().Select(getRootCategoriesHelper);
        }

        public IEnumerable<ItemResultDTO> GetItemsIn(string query, int? categoryId, int? carModelId)
        {
            var res = ItemRepository.GetItemsInCategoryAndModel(categoryId, carModelId);
            if (query != null)
            {
                var itemIds = Search.Query(query);
                res = res.Where(u => itemIds.Contains(u.Id));
            }

            return res.AsEnumerable().Select(itemHelper);
        }

        public IEnumerable<CarModelResultDTO> GetCarModels()
        {
            return CarModelRepository.GetAll().Select(u => ObjectHelpers.MapTo<CarModelResultDTO>(u));
        }

        private static byte[] binaryHelper<T>(IQueryable<T> baseQuery, Expression<Func<T, byte[]>> dataSelector,
            DateTime? cacheTime) where T : BaseModel
        {
            if (!baseQuery.Any())
            {
                throw new KeyNotFoundException();
            }

            try
            {
                var modifiedDate = baseQuery.Select(u => u.ModifiedDate).Single();
                return (cacheTime == null || modifiedDate > cacheTime) ? baseQuery.Select(dataSelector).Single() : null;
            }
            catch (Exception)
            {
                throw new IndexOutOfRangeException();
            }
        }

        public byte[] GetCarModelIcon(int carModelId, DateTime? cacheTime)
        {
            return binaryHelper(
                CarModelIconRepository.GetAll().Where(u => u.CarModelId == carModelId),
                u => u.data,
                cacheTime);
        }

        public byte[] GetItemIcon(int itemId, DateTime? cacheTime)
        {
            return binaryHelper(
                ItemPictureRepository.GetIconOfItem(itemId),
                u => u.Picture,
                cacheTime);
        }

        public byte[] GetPictureOfItem(int itemId, int index, DateTime? cacheTime)
        {
            return binaryHelper(
                ItemPictureRepository.GetPictureOfItem(itemId, index),
                u => u.Picture,
                cacheTime);
        }

        public bool MakeAPurchase(int UserId, IEnumerable<ItemPurchaseRequest> items)
        {
            lock (PurchaseLock)
            {
                var requestedItems = items.ToArray();
                var requestedItemsMap = requestedItems.ToDictionary(u => u.ItemId, u => u);
                var requestedItemIds = requestedItems.Select(u => u.ItemId);
                var sourceItems = ItemRepository.GetAll().Where(u => requestedItemIds.Contains(u.Id))
                    .ToDictionary(u => u.Id, u => u);
                if (sourceItems.Count != requestedItems.Length)
                {
                    throw new ArgumentException(null, nameof(items));
                }

                foreach (var (id, sourceItem) in sourceItems)
                {
                    var requestedItem = requestedItemsMap[id];
                    if (requestedItem.Quantity > sourceItem.Quantity)
                    {
                        return false;
                    }
                }

                var receit = new Receit
                {
                    UserId = UserId,
                    TotalPrice = sourceItems.Values.Sum(u => (u.Price * (1 - u.Discount)) * u.Quantity),
                    ItemCount = requestedItems.Length,
                    Items = requestedItems.Select(u =>
                    {
                        var sourceItem = sourceItems[u.ItemId];
                        sourceItem.Quantity -= u.Quantity;
                        ItemRepository.Update(sourceItem);
                        return new ReceitItem
                        {
                            ItemId = u.ItemId,
                            PriceAtPurchase = sourceItem.Price,
                            DiscountAtPurchase = sourceItem.Discount,
                            PurchasedQuantity = u.Quantity
                        };
                    }).ToList()
                };
                ReceitRepository.Insert(receit).Wait();
            }

            return true;
        }
    }
}