using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO.SqlDaoInterfaces
{
    public interface IInventoryDao
    {
        List<Inventory> GetInventories();
    }
}
