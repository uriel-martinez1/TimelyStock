using Capstone.DAO.SqlDaoInterfaces;
using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Capstone.DAO
{
    public class InventorySqlDao : IInventoryDao
    {
        private readonly string ConnectionString;
        private UserSqlDao userSqlDao;
        

        public InventorySqlDao(string connectionString)
        {
            ConnectionString = connectionString;
            userSqlDao = new UserSqlDao(connectionString);
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

        public Inventory CreateInventory(Inventory inventory)
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
                    // this is where we create a string that we convert to a int 
                    cmd.Parameters.AddWithValue("@userId", inventory.UserId);
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

        public Inventory UpdateInventory(int id, Inventory inventoryToUpdate)
        {
            Inventory updatedInventory = null;

            string sql = "UPDATE inventories " +
                "SET inventory_name = @inventoryName " +
                "WHERE inventory_id = @inventoryId;";
            try
            {
                using(SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@inventoryName", inventoryToUpdate.InventoryName);
                    cmd.Parameters.AddWithValue("@inventoryId", id);

                    int numberOfRowsAffected = cmd.ExecuteNonQuery();

                    if (numberOfRowsAffected == 0)
                    {
                        throw new DaoException("Zero rows affected, expected at least one");
                    }
                }
                updatedInventory = GetInventoryById(id);
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occured", ex);
            }
            return updatedInventory;
            
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
