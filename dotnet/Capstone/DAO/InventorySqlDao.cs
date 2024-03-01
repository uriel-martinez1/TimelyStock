using Capstone.DAO.SqlDaoInterfaces;
using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Exceptions;
using Microsoft.AspNetCore.Http;
using System;

namespace Capstone.DAO
{
    public class InventorySqlDao : IInventoryDao
    {
        private readonly string ConnectionString;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InventorySqlDao(string connectionString, IHttpContextAccessor httpContextAccessor)
        {
            ConnectionString = connectionString;
            _httpContextAccessor = httpContextAccessor;
        }

        public InventorySqlDao(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public List<Inventory> GetInventories()
        {
            List<Inventory> inventories = new List<Inventory>();

            string sql = "SELECT inventory_id, user_id, inventory_name " +
                "FROM inventories;";

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
                        inventories.Add(inventory);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred.", ex);
            }
            return inventories;
        }

        public List<Inventory> GetInventoriesByUserId(int userId)
        {
            List<Inventory> inventories = new List<Inventory>();

            string sql = "SELECT inventory_id, user_id, inventory_name " +
                "FROM inventories " +
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
                        Inventory inventory = MapRowToInventory(reader);
                        inventories.Add(inventory);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return inventories;
        }

        public Inventory CreatedInventory(Inventory inventory)
        {
            Inventory newInventory = new Inventory();

            string sql = "INSERT INTO inventories(user_id, inventory_name) " +
                "OUTPUT inserted.inventory_id " +
                "VALUES (@userId, @inventoryName);";

            try
            {
                int newInventoryId;

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    string userId = _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value; // this allows us to access the user identity 
                    cmd.Parameters.AddWithValue("@userId", int.Parse(userId)); 
                    cmd.Parameters.AddWithValue("@inventoryName", inventory.InventoryName);
                    newInventoryId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                newInventory = GetInventoryById(newInventoryId);
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL Exception occured", ex);
            }
            return newInventory;
        }

        public Inventory GetInventoryById(int inventoryId)
        {
            Inventory inventory = new Inventory();

            string sql = "SELECT inventory_id, user_id, inventory_name " +
                "FROM inventories " +
                "WHERE inventory_id = @inventoryId;";
            
            try
            {
                using(SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@inventoryId", inventoryId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        inventory = MapRowToInventory(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occured", ex);
            }
            return inventory;
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
