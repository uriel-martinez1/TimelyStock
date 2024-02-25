namespace Capstone.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string? ProductUrl { get; set; } // is nillable in Database design
        public int? SkuItemNumber { get; set; } // is nillable in Database design
        public decimal? Price { get; set; } // is nillable in Database design
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
    }
}
