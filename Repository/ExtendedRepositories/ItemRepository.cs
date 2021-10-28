using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Models;
using Models.DataModels.ItemModels;

namespace Repository.ExtendedRepositories
{
    public interface IItemRepository : IRepository<Item>
    {
        IQueryable<Item> GetItemsInCategory(ItemCategory category);
        IQueryable<Item> GetItemsInCategoryAndModel(int? categoryId, int? carModelId);
    }

    public class ItemRepository : Repository<Item>, IItemRepository
    {
        private readonly IItemCategoryRepository ItemCategoryRepository;

        public ItemRepository(ApplicationDbContext context, ILogger<ItemRepository> logger,
            IItemCategoryRepository itemCategoryRepository) : base(context, logger)
        {
            this.ItemCategoryRepository = itemCategoryRepository;
        }

        private static void getCategoryIdsHelper(ItemCategory category, ICollection<int> sink)
        {
            sink.Add(category.Id);
            foreach (var child in category.ChildrenCategories)
            {
                getCategoryIdsHelper(child, sink);
            }
        }

        public IQueryable<Item> GetItemsInCategory(ItemCategory category)
        {
            var ids = new List<int>();
            getCategoryIdsHelper(category, ids);
            return GetAll().Where(u => ids.Contains(u.CategoryId));
        }

        public IQueryable<Item> GetItemsInCategoryAndModel(int? categoryId, int? carModelId)
        {
            var query = GetAll();
            if (carModelId != null) query = query.Where(u => u.CarModelId == carModelId);
            if (categoryId != null)
            {
                var categoryIds = new List<int>();
                getCategoryIdsHelper(ItemCategoryRepository.Get(categoryId.Value), categoryIds);
                query = query.Where(u => categoryIds.Contains(u.CategoryId));
            }

            return query;
        }
    }
}