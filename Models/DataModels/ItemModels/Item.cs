using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Models.DataModels.ItemModels
{
    public class Item : BaseModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        
        public int CategoryId { get; set; }
        public virtual ItemCategory Category { get; set; }
        
        public int CarModelId { get; set; }
        public virtual ItemCarModel CarModel { get; set; }

        [NotMapped]
        public double TotalRating
        {
            get => Purchases.Where(u => u.Rate != null).Select(u => u.Rate.Rate).Average();
        }
        [NotMapped]
        public int RateCount
        {
            get => Purchases.Count(u => u.Rate != null);
        }
        [NotMapped]
        public int PurchaseCount
        {
            get => Purchases.Count;
        }
        
        public virtual ICollection<ItemPurchase> Purchases { get; set; }
        public virtual ICollection<ItemPicture> Pictures { get; set; }
    }
}