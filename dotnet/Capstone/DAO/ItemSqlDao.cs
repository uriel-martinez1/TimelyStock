using Capstone.DAO.SqlDaoInterfaces;
using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Exceptions;
using System;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;

namespace Capstone.DAO
{
    public class ItemSqlDao : IItemDao
    {
        private readonly string ConnectionString;

        public ItemSqlDao(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public List<Item> GetItemsByUserId(int userId)
        {
            List<Item> output = new List<Item>();

            string sql = "SELECT items.item_id, item_name, product_url, sku_item_number, price, available_quantity, reorder_point, reorder_quantity, category_id, supplier_id  FROM items " +
                "JOIN inventory_items ON items.item_id = inventory_items.item_id " +
                "JOIN inventories ON inventory_items.inventory_id = inventories.inventory_id " +
                "JOIN users ON users.user_id = inventories.user_id " +
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
                        Item item = MapRowToItem(reader);
                        output.Add(item);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new DaoException("SQL exception occurred.", e);
            }
            return output;
        }

        public List<Item> GetItemsByInventoryId(int inventoryId)
        {
            List<Item> output = new List<Item>();

            string sql = "SELECT items.item_id, item_name, product_url, sku_item_number, price, available_quantity, reorder_point, reorder_quantity, category_id, supplier_id FROM items " +
                "JOIN inventory_items ON items.item_id = inventory_items.item_id " +
                "JOIN inventories ON inventory_items.inventory_id = inventories.inventory_id " +
                "WHERE inventories.inventory_id = @inventoryId;";

            try
            {
                using(SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@inventoryId", inventoryId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Item item = MapRowToItem(reader);
                        output.Add(item);
                    }
                }
            }
            catch (SqlException e)
            {
                throw new DaoException("SQL exception occured.", e);
            }
            return output;
        }

        public List<Item> GetItemsByName(string name, bool useWildCard)
        {
            List<Item> output = new List<Item>();

            if (useWildCard)
            {
                name = "%" + name + "%";
            }

            string sql = "SELECT item_id, item_name, product_url, sku_item_number, price, available_quantity, reorder_point, reorder_quantity, category_id, supplier_id FROM items " +
                "WHERE item_name LIKE @name;";

            try
            {
                using(SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Item item = MapRowToItem(reader);
                        output.Add(item);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQl exception occured", ex);
            }
            return output;
        }

        public Item CreateItem(Item item)
        {
            Item newItem = new Item();

            string sql = "INSERT INTO items(item_name, product_url, sku_item_number, price, available_quantity, reorder_point, reorder_quantity, category_id, supplier_id) " +
                "OUTPUT inserted.item_id " +
                "VALUES (@itemName, @productUrl, @skuNumber, @price, @availableQty, @reorderPoint, @reorderQty, @categoryId, @supplierId)";

            try
            {
                int newItemId;

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@itemName", item.ItemName);
                    cmd.Parameters.AddWithValue("@productUrl", item.ProductUrl);
                    cmd.Parameters.AddWithValue("@skuNumber", item.SkuItemNumber);
                    cmd.Parameters.AddWithValue("@price", item.Price);
                    cmd.Parameters.AddWithValue("@availableQty", item.AvailableQuantity);
                    cmd.Parameters.AddWithValue("@reorderPoint", item.ReorderPoint);
                    cmd.Parameters.AddWithValue("@reorderQty", item.ReorderQuantity);
                    cmd.Parameters.AddWithValue("@categoryId", item.CategoryId);
                    cmd.Parameters.AddWithValue("@supplierId", item.SupplierId);
                    newItemId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                newItem = GetItemById(newItemId);
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL Exception occured", ex);
            }
            return newItem;
        }

        public Item GetItemById(int itemId)
        {
            Item item = new Item();

            string sql = "SELECT item_id, item_name, product_url, sku_item_number, price, available_quantity, reorder_point, reorder_quantity, category_id, supplier_id " +
                "FROM items " +
                "WHERE item_id = @itemId;";

            try
            {
                using(SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@itemId", itemId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        item = MapRowToItem(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occured", ex);
            }
            return item;
        }

        public bool ConfirmUpdatedItemFoundInInventory(int inventoryId, Item updatedItem)
        {
            string sql = "SELECT COUNT(*) FROM items " +
                "JOIN inventory_items ON items.item_id = inventory_items.item_id " +
                "JOIN inventories ON inventories.inventory_id = inventory_items.inventory_id " +
                "WHERE inventories.inventory_id = @inventoryId AND items.item_id = @itemId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@inventoryId", inventoryId);
                    cmd.Parameters.AddWithValue("@itemId", updatedItem.ItemId);

                    //Execute scalar query to get the count of rows
                    int rowCount = (int)cmd.ExecuteScalar();

                    // If rowCount is 1, it means the updated item is found
                    bool itemFound = rowCount == 1;

                    return itemFound;
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occured", ex);
            }
        }

        // maybe extra security in the sql statement to confirm that inventoryId AND the itemId match the item to be updated?
        public Item UpdateItem(int itemId, Item itemToUpdate)
        {
            Item updatedItem = null;

            string sql = "UPDATE items " +
                "SET item_name = @itemName, product_url = @productUrl, sku_item_number = @skuNumber, price = @price, available_quantity = @availableQty, reorder_point = @reorderPoint, reorder_quantity = @reorderQty, category_id = @categoryId, supplier_id = @supplierId " +
                "WHERE item_id = @itemId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@itemName", itemToUpdate.ItemName);
                    cmd.Parameters.AddWithValue("@productUrl", itemToUpdate.ProductUrl);
                    cmd.Parameters.AddWithValue("@skuNumber", itemToUpdate.SkuItemNumber);
                    cmd.Parameters.AddWithValue("@price", itemToUpdate.Price);
                    cmd.Parameters.AddWithValue("@availableQty", itemToUpdate.AvailableQuantity);
                    cmd.Parameters.AddWithValue("@reorderPoint", itemToUpdate.ReorderPoint);
                    cmd.Parameters.AddWithValue("@reorderQty", itemToUpdate.ReorderQuantity);
                    cmd.Parameters.AddWithValue("@categoryId", itemToUpdate.CategoryId);
                    cmd.Parameters.AddWithValue("@supplierId", itemToUpdate.SupplierId);
                    cmd.Parameters.AddWithValue("@itemId", itemToUpdate.ItemId);

                    int numberOfRowsAffected = cmd.ExecuteNonQuery();

                    if(numberOfRowsAffected == 0)
                    {
                        throw new DaoException("Zero rows affected, expected at least one");
                    }
                }
                updatedItem = GetItemById(itemId);
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occured", ex);
            }
            return updatedItem;
        }

        public bool LinkItemInventory(int inventoryId, int itemId)
        {
            string sql = "INSERT INTO inventory_items(inventory_id, item_id) " +
                "VALUES (@inventoryId, @itemId);";

            try
            {
                using(SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@inventoryId", inventoryId);
                    cmd.Parameters.AddWithValue("@itemId", itemId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception has ocurred: ", ex);
            }
        }

        public int DeleteItemByInventoryIdAndItemId(int inventoryId, int itemId)
        {
            int numberOfRowsAffected = 0;

            string inventoriesItemsSql = "DELETE FROM inventory_items WHERE inventory_id = @inventoryId AND item_id = @itemId;";
            string itemSql = "DELETE FROM items WHERE item_id = @itemId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    // delete the reference of the item in the cross table
                    SqlCommand inventoryItemsCmd = new SqlCommand(inventoriesItemsSql, conn);
                    inventoryItemsCmd.Parameters.AddWithValue("@inventoryId", inventoryId);
                    inventoryItemsCmd.Parameters.AddWithValue("@itemId", itemId);
                    inventoryItemsCmd.ExecuteNonQuery();

                    // now lets delete the item
                    SqlCommand itemCmd = new SqlCommand(itemSql, conn);
                    itemCmd.Parameters.AddWithValue("itemId", itemId);

                    numberOfRowsAffected = itemCmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return numberOfRowsAffected;
        }

        public Item MapRowToItem(SqlDataReader reader)
        {
            Item newItem = new Item();
            newItem.ItemId = Convert.ToInt32(reader["item_id"]);
            newItem.ItemName = Convert.ToString(reader["item_name"]);

            if (reader["product_url"] is DBNull)    // Nullable value
            {
                newItem.ProductUrl = null;
            }
            else
            {
                newItem.ProductUrl = Convert.ToString(reader["product_url"]);   
            }

            if (reader["sku_item_number"] is DBNull)    // Nullable value
            {
                newItem.SkuItemNumber = null;
            }
            else
            {
                newItem.SkuItemNumber = Convert.ToString(reader["sku_item_number"]);
            }

            if (reader["price"] is DBNull)    // Nullable value
            {
                newItem.Price = null;
            }
            else
            {
                newItem.Price = Convert.ToDecimal(reader["price"]);
            }

            newItem.AvailableQuantity = Convert.ToInt32(reader["available_quantity"]);
            newItem.ReorderPoint = Convert.ToInt32(reader["reorder_point"]);
            newItem.ReorderQuantity = Convert.ToInt32(reader["reorder_quantity"]);
            newItem.CategoryId = Convert.ToInt32(reader["category_id"]); 
            newItem.SupplierId = Convert.ToInt32(reader["supplier_id"]);
            return newItem;

        }
    }
}
