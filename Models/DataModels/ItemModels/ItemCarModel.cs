using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.DataModels.ItemModels
{
    public class ItemCarModel : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ItemCarModelIcon Icon { get; set; }
    }
}