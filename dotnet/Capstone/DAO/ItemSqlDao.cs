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
        // TODO: Confirm the connection string is pointing to the correct database
        private readonly string ConnectionString;

        public ItemSqlDao(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public List<Item> GetItems()
        {
            List<Item> output = new List<Item>();

            string sql = "SELECT item_id, item_name, product_url, sku_item_number, price, category_id, supplier_id FROM items;";

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

        public Item MapRowToItem(SqlDataReader reader)
        {
            Item newItem = new Item();
            newItem.ItemId = Convert.ToInt32(reader["item_id"]);
            newItem.ItemName = Convert.ToString(reader["item_name"]);
            newItem.ProductUrl = Convert.ToString(reader["product_url"]); // Unsure about if it will cause error since being a varchar(2083) in db
            newItem.SkuItemNumber = Convert.ToInt32(reader["sku_item_number"]);
            newItem.Price = Convert.ToDecimal(reader["price"]);
            newItem.CategoryId = Convert.ToInt32(reader["category_id"]); 
            newItem.SupplierId = Convert.ToInt32(reader["supplier_id"]);
            return newItem;

        }
    }
}
