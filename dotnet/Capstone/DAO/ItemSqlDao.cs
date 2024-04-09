﻿using Capstone.DAO.SqlDaoInterfaces;
using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Exceptions;
using System;
using System.Xml.Linq;

namespace Capstone.DAO
{
    public class ItemSqlDao : IItemDao
    {
        private readonly string ConnectionString;

        public ItemSqlDao(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public List<Item> GetItems()
        {
            List<Item> output = new List<Item>();

            string sql = "SELECT item_id, item_name, product_url, sku_item_number, price, available_quantity, reorder_quantity, category_id, supplier_id FROM items;";

            try
            {
                using(SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
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

            string sql = "SELECT items.item_id, item_name, product_url, sku_item_number, price, available_quantity, reorder_quantity, category_id, supplier_id FROM items " +
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

            string sql = "SELECT item_id, item_name, product_url, sku_item_number, price, available_quantity, reorder_quantity, category_id, supplier_id FROM items " +
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

            string sql = "INSERT INTO items(item_name, product_url, sku_item_number, price, available_quantity, reorder_quantity, category_id, supplier_id) " +
                "OUTPUT inserted.item_id " +
                "VALUES (@itemName, @productUrl, @skuNumber, @price, @availableQty, @reorderQty, @categoryId, @supplierId)";

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

            string sql = "SELECT item_id, item_name, product_url, sku_item_number, price, available_quantity, reorder_quantity, category_id, supplier_id " +
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
                newItem.SkuItemNumber = Convert.ToInt32(reader["sku_item_number"]);
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
            newItem.ReorderQuantity = Convert.ToInt32(reader["reorder_quantity"]);
            newItem.CategoryId = Convert.ToInt32(reader["category_id"]); 
            newItem.SupplierId = Convert.ToInt32(reader["supplier_id"]);
            return newItem;

        }
    }
}
