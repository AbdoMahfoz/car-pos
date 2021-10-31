using System;
using BusinessLogic.Interfaces;

namespace BusinessLogic.Initializers
{
    public class IndexInitializer : BaseInitializer
    {
        private readonly ISearch Search;

        public IndexInitializer(ISearch search)
        {
            Search = search;
        }

        protected override void Initialize()
        {
            Console.WriteLine("Indexing Products");
            Search.UpdateIndex().Wait();
            Console.WriteLine("Indexing Complete");
        }
    }
}