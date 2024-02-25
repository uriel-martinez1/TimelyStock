using Capstone.DAO.SqlDaoInterfaces;
using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Exceptions;
using System;

namespace Capstone.DAO
{
    public class InventorySqlDao : IInventoryDao
    {
        private readonly string ConnectionString;

        public InventorySqlDao(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public List<Inventory> GetInventories()
        {
            List<Inventory> output = new List<Inventory>();

            string sql = "SELECT inventory_id, user_id, inventory_name FROM inventories;";

            try
            {
                using(SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Inventory inventory = MapRowToInventory(reader);
                        output.Add(inventory);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred.", ex);
            }
            return output;
        }

        public Inventory MapRowToInventory(SqlDataReader reader)
        {
            Inventory inventory = new Inventory();
            inventory.InventoryId = Convert.ToInt32(reader["inventory_id"]);
            inventory.UserId = Convert.ToInt32(reader["user_id"]);
            inventory.InventoryName = Convert.ToString(reader["inventory_name"]);
            return inventory;
        }
    }
}
