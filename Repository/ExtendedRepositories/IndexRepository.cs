using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Models;
using Models.DataModels.SearchModels;

namespace Repository.ExtendedRepositories
{
    public interface IIndexRepository : IRepository<Index>
    {
        IQueryable<string> GetTokens();
        IDictionary<string, IEnumerable<int>> GetTokensWithItemIds();
    }

    public class IndexRepository : Repository<Index>, IIndexRepository
    {
        public IndexRepository(ApplicationDbContext context, ILogger<IndexRepository> logger) : base(context, logger)
        {
        }

        public IQueryable<string> GetTokens()
        {
            return GetAll().Select(u => u.Token).Distinct();
        }

        public IDictionary<string, IEnumerable<int>> GetTokensWithItemIds()
        {
            return GetAll().Select(u => new { u.Token, Items = u.Items.Select(x => x.ItemId) })
                .ToArray()
                .ToDictionary(u => u.Token, u => u.Items);
        }
    }
}