namespace Capstone.Models
{
    public class InventoryItems
    {
        public int InventoryId { get; set; }
        public int ItemId { get; set; }
        public int AvailableQuantity { get; set; }
        public int ReorderQuantity { get; set; }
    }
}
