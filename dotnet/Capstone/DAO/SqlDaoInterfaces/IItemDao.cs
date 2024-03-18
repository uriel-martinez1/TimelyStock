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
    }
}
