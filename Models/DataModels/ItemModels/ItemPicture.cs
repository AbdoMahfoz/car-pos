namespace Models.DataModels.ItemModels
{
    public class ItemPicture : BaseModel
    {
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public bool IsIcon { get; set; }
        public bool IsCover { get; set; }
        public byte[] Picture { get; set; }
    }
}