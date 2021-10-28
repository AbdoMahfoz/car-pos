using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Models.DataModels.ItemModels
{
    public class Receit : BaseModel
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [NotMapped]
        public double TotalPrice
        {
            get => Items.Sum(u => (u.PriceAtPurchase * (1 - u.DiscountAtPurchase)) * u.PurchasedQuantity);
        }

        [NotMapped]
        public int ItemCount
        {
            get => Items.Count;
        }

        public virtual ICollection<ReceitItem> Items { get; set; }
    }
}