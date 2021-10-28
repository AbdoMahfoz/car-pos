using System.Linq;
using Microsoft.Extensions.Logging;
using Models;
using Models.DataModels.ItemModels;

namespace Repository.ExtendedRepositories
{
    public interface IItemPictureRepository : IRepository<ItemPicture>
    {
        IQueryable<ItemPicture> GetPictureOfItem(int itemId, int idx);
        IQueryable<ItemPicture> GetIconOfItem(int itemId);

    }

    public class ItemPictureRepository : Repository<ItemPicture>, IItemPictureRepository
    {
        public ItemPictureRepository(ApplicationDbContext context, ILogger<ItemPictureRepository> logger) : base(
            context, logger)
        {
        }

        public IQueryable<ItemPicture> GetPictureOfItem(int itemId, int idx)
        {
            return GetAll().Where(u => u.ItemId == itemId && !u.IsIcon).OrderBy(u => u.idx)
                .Skip(idx).Take(1);
        }

        public IQueryable<ItemPicture> GetIconOfItem(int itemId)
        {
            return GetAll().Where(u => u.ItemId == itemId && u.IsIcon);
        }
    }
}