namespace Models.DataModels
{
    public class ItemRate : BaseModel
    {
        public string Comment { get; set; }
        public int Rate { get; set; }
        public int PurchaseId { get; set; }
        public virtual ItemPurchase Purchase { get; set; }
    }
}