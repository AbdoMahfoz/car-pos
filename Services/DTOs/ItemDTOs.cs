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
        public int Id { get; set; } 
        public string Name { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public string CategoryName { get; set; }
        public int CarModelId { get; set; }
        public bool IsNew { get; set; }
        public int GuaranteeYears { get; set; }
        public int Quantity { get; set; }
        public int ImageCount { get; set; }
    }

    public class CarModelResultDTO
    {
        public int Id { get; set; }
        public int Name { get; set; }
    }

    public class ItemPurchaseRequest
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}