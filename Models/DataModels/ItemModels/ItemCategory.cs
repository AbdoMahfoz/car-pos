using System.Collections.Generic;
using Models.DataModels.ItemModels;

namespace Models.DataModels
{
    public class ItemCategory : BaseModel
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public virtual ItemCategory Parent { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}