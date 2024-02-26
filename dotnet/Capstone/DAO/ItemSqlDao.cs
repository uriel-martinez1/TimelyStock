using Capstone.DAO.SqlDaoInterfaces;
using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Exceptions;
using System;

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
            catch (Exception)
            {

                throw;
            }
            return output;
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
