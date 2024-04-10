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

        //link method that links the newly created item to the existing inventory
        bool LinkItemInventory(int inventoryId, int itemId);
    }
}
