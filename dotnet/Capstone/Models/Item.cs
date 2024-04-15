namespace Capstone.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string? ProductUrl { get; set; } // is nullable in Database design
        public int? SkuItemNumber { get; set; } // is nullable in Database design
        public decimal? Price { get; set; } // is nullable in Database design 
        public int AvailableQuantity { get; set; }
        public int ReorderPoint { get; set; }
        public int ReorderQuantity { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
    }
}
