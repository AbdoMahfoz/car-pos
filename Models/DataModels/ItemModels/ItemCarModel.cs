using System.Collections.Generic;

namespace Models.DataModels.ItemModels
{
    public class ItemCarModel : BaseModel
    {
        public string Name { get; set; }
        public byte[] Logo { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}