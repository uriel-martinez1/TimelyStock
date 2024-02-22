namespace Capstone.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int AvailableQuanity { get; set; }
        public int ReorderQuantity { get; set; }
        public bool ReorderItem { get; set; }
        public string ProductUrl { get; set; }
        public int ItemNumber { get; set; }
        public int DepartmentId { get; set; }
        public int SupplierId { get; set; }
    }
}
