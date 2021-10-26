using System.Collections.Generic;

namespace Models.DataModels.ItemModels
{
    public class ItemPurchase : BaseModel
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public double PriceAtPurchase { get; set; }
        public double DiscountAtPurchase { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
        public int PurchasedQuantity { get; set; }
    }
}