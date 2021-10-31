using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using Models.DataModels.SearchModels;
using Repository.ExtendedRepositories;
using Index = Models.DataModels.SearchModels.Index;

namespace BusinessLogic.Implementations
{
    public class Search : ISearch
    {
        private readonly IIndexRepository IndexRepository;
        private readonly IItemRepository ItemRepository;
        private static IDictionary<string, IEnumerable<int>> tokensWithItemIds;
        private readonly static object IndexLock = new object();

        private IDictionary<string, IEnumerable<int>> TokensWithItemIds
        {
            get
            {
                lock (IndexLock)
                {
                    return tokensWithItemIds ??= IndexRepository.GetTokensWithItemIds();
                }
            }
        }

        public Search(IIndexRepository indexRepository, IItemRepository itemRepository)
        {
            IndexRepository = indexRepository;
            ItemRepository = itemRepository;
        }

        private static IEnumerable<string> tokenize(string query)
        {
            return query.Trim().ToLower().Split(' ');
        }

        private static int lcs(string first, string second)
        {
            int[,] dp = new int[first.Length + 2, second.Length + 2];
            for (int i = first.Length; i >= 0; i--)
            {
                for (int j = second.Length; j >= 0; j--)
                {
                    if (i == first.Length || j == second.Length)
                    {
                        dp[i, j] = 0;
                    }
                    else if (first[i] == second[j])
                    {
                        dp[i, j] = dp[i + 1, j + 1] + 1;
                    }
                    else
                    {
                        dp[i, j] = Math.Max(dp[i + 1, j], Math.Max(dp[i, j + 1], dp[i + 1, j + 1]));
                    }
                }
            }

            return dp[0, 0];
        }

        public Task UpdateIndex() => Task.Run(() =>
        {
            /*
             * This method aggressivly updates index by removing all previous elements and recreate the index
             * TODO: Incremental Index Update
             */
            lock (IndexLock)
            {
                var res = new Dictionary<string, List<int>>();
                foreach (var item in ItemRepository.GetAll().ToArray())
                {
                    var tokens = tokenize(item.Name)
                        .Concat(tokenize(item.Category.Name))
                        .Concat(tokenize(item.CarModel.Name));
                    foreach (var token in tokens)
                    {
                        if (!res.TryAdd(token, new List<int> { item.Id }))
                        {
                            res[token].Add(item.Id);
                        }
                    }
                }

                if (res.Count == 0) return;
                var currentIndex = IndexRepository.GetAll();
                if (currentIndex.Any())
                {
                    IndexRepository.HardDeleteRange(IndexRepository.GetAll());
                }

                tokensWithItemIds = null;
                IndexRepository.InsertRange(res.Select(u => new Index
                {
                    Token = u.Key,
                    Items = u.Value.Select(x => new IndexItems { ItemId = x }).ToList()
                })).Wait();
            }
        });

        public IEnumerable<int> Query(string query)
        {
            var queryTokens = tokenize(query);
            var data = TokensWithItemIds;
            var res = new Dictionary<int, int>();
            foreach (var q in queryTokens)
            {
                var orderedPairs =
                    data.OrderByDescending(u => lcs(q, u.Key)).Take(5);
                int score = 5;
                foreach (var resPair in orderedPairs)
                {
                    foreach (var resItemId in resPair.Value)
                    {
                        if (!res.TryAdd(resItemId, score))
                        {
                            res[resItemId] += score;
                        }
                    }

                    score--;
                }
            }

            return res.OrderByDescending(u => u.Value).Select(u => u.Key).Take(20);
        }
    }
}