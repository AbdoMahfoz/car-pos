using System.Collections.Generic;
using Models.DataModels.ItemModels;

namespace Models.DataModels.SearchModels
{
    public class IndexItems
    {
        public int IndexId { get; set; }
        public virtual Index Index { get; set; }
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
    }
}