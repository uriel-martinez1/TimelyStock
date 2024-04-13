using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO.SqlDaoInterfaces
{
    public interface ISupplierDao
    {
        List<Supplier> GetSuppliers();
        List<Supplier> GetSuppliersByUserId(int userId);
        Supplier CreateSupplier(Supplier supplier);
        Supplier GetSupplierById(int supplierId);
    }
}
