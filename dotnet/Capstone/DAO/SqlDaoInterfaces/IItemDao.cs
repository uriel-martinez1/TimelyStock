using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO.SqlDaoInterfaces
{
    public interface IItemDao
    {
        List<Item> GetItemsByUserId(int userId);
        List<Item> GetItemsByName(string name, bool useWildCard);
        List<Item> GetItemsByInventoryId(int inventoryId);
        Item GetItemByInventoryIdAndItemId(int inventoryId, int itemId);
        Item CreateItem(Item item);
        Item GetItemById(int itemId);
        // Not sure about how to utilize the inventory Id here?
        Item UpdateItem(int itemId, Item itemToUpdate);
        bool LinkItemInventory(int inventoryId, int itemId);
        bool ConfirmUpdatedItemFoundInInventory(int inventoryId, Item updatedItem);
        int DeleteItemByInventoryIdAndItemId(int inventoryId, int itemId);
    }
}
