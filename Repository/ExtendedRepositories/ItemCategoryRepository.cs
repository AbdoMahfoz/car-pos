using System.Linq;
using Microsoft.Extensions.Logging;
using Models;
using Models.DataModels.ItemModels;

namespace Repository.ExtendedRepositories
{
    public interface IItemCategoryRepository : IRepository<ItemCategory>
    {
        IQueryable<ItemCategory> GetRootCategories();
    }

    public class ItemCategoryRepository : Repository<ItemCategory>, IItemCategoryRepository
    {
        public ItemCategoryRepository(ApplicationDbContext context, ILogger<ItemCategoryRepository> logger) : base(
            context, logger)
        {
        }

        public IQueryable<ItemCategory> GetRootCategories() => GetAll().Where(u => u.ParentId == null);
    }
}