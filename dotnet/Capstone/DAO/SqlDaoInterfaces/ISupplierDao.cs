using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO.SqlDaoInterfaces
{
    public interface ISupplierDao
    {
        List<Supplier> GetSuppliers();
    }
}
