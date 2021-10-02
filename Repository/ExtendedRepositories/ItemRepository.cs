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
    }
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(ApplicationDbContext context, ILogger<ItemRepository> logger) : base(context, logger)
        {
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
    }
}