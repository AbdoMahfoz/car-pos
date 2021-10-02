using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DataModels.ItemModels
{
    public class ItemCategory : BaseModel
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }

        [NotMapped]
        public bool IsLeaf
        {
            get => ChildrenCategories.Count == 0;
        }
        
        public virtual ItemCategory Parent { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<ItemCategory> ChildrenCategories { get; set; }
    }
}