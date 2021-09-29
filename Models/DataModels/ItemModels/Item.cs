using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Models.DataModels.ItemModels
{
    public class Item : BaseModel
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public virtual ItemCategory Category { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }

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
    }
}