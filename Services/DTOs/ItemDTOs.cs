using System.Collections.Generic;

namespace Services.DTOs
{
    public class ItemCategoryResultDTO
    {
        public string Name { get; set; }
        public bool IsLeaf { get; set; }
        public IEnumerable<ItemCategoryResultDTO> Children { get; set; }
    }

    public class ItemResultDTO
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public string CategoryName { get; set; }
        public string CarModelName { get; set; }
        public double TotalRating { get; set; }
        public int RateCount { get; set; }
        public int PurchaseCount { get; set; }
    }

    public class ItemPictureResultDTO
    {
        public string b64Cover { get; set; }
        public string b64Icon { get; set; }
        public string b64Pictures { get; set; }
    }

    public class CarModelResultDTO
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public bool ShouldUpdatePictureCache { get; set; }
    }
}