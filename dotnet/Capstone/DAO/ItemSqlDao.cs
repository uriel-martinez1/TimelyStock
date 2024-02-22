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

            string sql = "SELECT item_id, item_name, available_quantity, reorder_quantity, reorder_item, product_url, item_number, department_id, supplier_id FROM items;";

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
            newItem.AvailableQuanity = Convert.ToInt32(reader["available_quantity"]);
            newItem.ReorderQuantity = Convert.ToInt32(reader["reorder_quantity"]);
            newItem.ReorderItem = Convert.ToBoolean(reader["reorder_item"]);
            newItem.ProductUrl = Convert.ToString(reader["product_url"]); // Unsure about if it will cause error since being a varchar(2083) in db
            newItem.ItemNumber = Convert.ToInt32(reader["item_number"]); 
            newItem.DepartmentId = Convert.ToInt32(reader["department_id"]); 
            newItem.SupplierId = Convert.ToInt32(reader["supplier_id"]);
            return newItem;

        }
    }
}
