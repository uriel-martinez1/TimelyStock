﻿using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO.SqlDaoInterfaces
{
    public interface IInventoryDao
    {
        List<Inventory> GetInventories();
        List<Inventory> GetInventoriesByUserId(int userId);
        Inventory CreateInventory(Inventory inventory);
        Inventory GetInventoryById(int inventoryId);
        Inventory UpdateInventory(Inventory inventoryToUpdate);
    }
}
