using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO.SqlDaoInterfaces
{
    public interface IItemDao
    {
        List<Item> GetItems();
        List<Item> GetItemsByName(string name, bool useWildCard);
        List<Item> GetItemsByInventoryId(int inventoryId);
        Item CreateItem(Item item);
        Item GetItemById(int itemId);
        // Not sure about how to utilize the inventory Id here?
        Item UpdateItem(int inventoryId, Item itemToUpdate);
        bool LinkItemInventory(int inventoryId, int itemId);
    }
}
