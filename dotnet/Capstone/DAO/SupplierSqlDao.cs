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
        private UserSqlDao userSqlDao;

        public SupplierSqlDao(string connectionString)
        {
            ConnectionString = connectionString;
            userSqlDao = new UserSqlDao(connectionString);
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

        public List<Supplier> GetSuppliersByUserId(int userId)
        {
            List<Supplier> suppliers = new List<Supplier>();

            string sql = "SELECT DISTINCT suppliers.supplier_id, supplier_name FROM suppliers " +
                "JOIN items ON suppliers.supplier_id = items.supplier_id " +
                "JOIN inventory_items ON items.item_id = inventory_items.item_id " +
                "JOIN inventories ON inventory_items.inventory_id = inventories.inventory_id " +
                "JOIN users ON inventories.user_id = users.user_id " +
                "WHERE users.user_id = @userId;";

            try
            {
                using(SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Supplier supplier = MapRowToSupplier(reader);
                        suppliers.Add(supplier);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occured", ex);
            }
            return suppliers;
        }

        public Supplier CreateSupplier(Supplier supplier)
        {
            Supplier newSupplier = new Supplier();

            string sql = "INSERT INTO suppliers(supplier_name) " +
                "OUTPUT inserted.supplier_id " +
                "VALUES (@supplierName)";

            try
            {
                int newSupplierId;

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@supplierName", supplier.SupplierName);
                    newSupplierId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                newSupplier = GetSupplierById(newSupplierId);
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL Exception ocurred", ex);
            }
            return newSupplier;
        }

        public Supplier GetSupplierById(int supplierId)
        {
            Supplier supplier = new Supplier();

            string sql = "SELECT supplier_id,supplier_name " +
                "FROM suppliers " +
                "WHERE supplier_id = @supplierId";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@supplierId", supplierId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        supplier = MapRowToSupplier(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL Exception occured", ex);
            }
            return supplier;
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
