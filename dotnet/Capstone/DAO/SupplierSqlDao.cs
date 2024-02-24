using Capstone.DAO.SqlDaoInterfaces;
using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Exceptions;
using System;

namespace Capstone.DAO
{
    public class SupplierSqlDao : ISupplierDao
    {
        private readonly string ConnectionString;

        public SupplierSqlDao(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public List<Supplier> GetSuppliers()
        {
            List<Supplier> output = new List<Supplier>();

            string sql = "SELECT supplier_id, supplier_name FROM suppliers;";

            try
            {
                using(SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Supplier supplier = MapRowToSupplier(reader);
                        output.Add(supplier);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return output;
        }

        public Supplier MapRowToSupplier(SqlDataReader reader)
        {
            Supplier newSupplier = new Supplier();
            newSupplier.SupplierId = Convert.ToInt32(reader["supplier_id"]);
            newSupplier.SupplierName = Convert.ToString(reader["supplier_name"]);
            return newSupplier;
        }
    }
}
