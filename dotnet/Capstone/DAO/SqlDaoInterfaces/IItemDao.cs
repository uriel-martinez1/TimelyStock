using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO.SqlDaoInterfaces
{
    public interface IItemDao
    {
        List<Item> GetItems();
    }
}
