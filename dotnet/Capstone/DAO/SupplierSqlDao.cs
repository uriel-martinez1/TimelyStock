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

            string sql = "SELECT supplier_id, user_id, supplier_name FROM suppliers;";

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

        //TODO - Verify that this is working! 
        public List<Supplier> GetSuppliersByUserId(int userId)
        {
            List<Supplier> suppliers = new List<Supplier>();

            string sql = "SELECT DISTINCT supplier_id, user_id, supplier_name FROM suppliers " +
                "WHERE user_id = @userId;";

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

            string sql = "INSERT INTO suppliers(user_id, supplier_name) " +
                "OUTPUT inserted.supplier_id " +
                "VALUES (@userId, @supplierName)";

            try
            {
                int newSupplierId;

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userId", supplier.UserId);
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

            string sql = "SELECT supplier_id, user_id, supplier_name " +
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
            newSupplier.UserId = Convert.ToInt32(reader["user_id"]);
            newSupplier.SupplierName = Convert.ToString(reader["supplier_name"]);
            return newSupplier;
        }
    }
}
