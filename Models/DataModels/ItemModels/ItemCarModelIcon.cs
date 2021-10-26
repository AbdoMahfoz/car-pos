namespace Models.DataModels.ItemModels
{
    public class ItemCarModelIcon : BaseModel
    {
        public int CarModelId { get; set; }
        public virtual ItemCarModel CarModel { get; set; }
        public byte[] data { get; set; }
    }
}