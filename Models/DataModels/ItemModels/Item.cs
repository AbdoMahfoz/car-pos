using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Models.DataModels.SearchModels;

namespace Models.DataModels.ItemModels
{
    public class Item : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public bool IsNew { get; set; }
        public int GuaranteeYears { get; set; }
        public int Quantity { get; set; }

        [NotMapped]
        public int ImageCount
        {
            get => Pictures.Count(u => !u.IsIcon);
        }
        
        public int CategoryId { get; set; }
        public virtual ItemCategory Category { get; set; }
        
        public int CarModelId { get; set; }
        public virtual ItemCarModel CarModel { get; set; }

        
        public virtual ICollection<ReceitItem> Purchases { get; set; }
        public virtual ICollection<ItemPicture> Pictures { get; set; }
        public virtual ICollection<IndexItems> Indices { get; set; }
    }
}