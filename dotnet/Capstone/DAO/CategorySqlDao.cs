using Capstone.DAO.SqlDaoInterfaces;
using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Exceptions;
using System;

namespace Capstone.DAO
{
    //ORM - Entity Framework - system that writes SQL for you
    public class CategorySqlDao : ICategoryDao
    {
        private readonly string ConnectionString;

        public CategorySqlDao(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public List<Category> GetCategories()
        {
            List<Category> output = new List<Category>();

            string sql = "SELECT category_id, category_name FROM categories;";

            try
            {
                using(SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Category category = MapRowToCategory(reader);
                        output.Add(category);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occured.", ex);
            }
            return output;
        }

        public Category MapRowToCategory(SqlDataReader reader)
        {
            Category newCategory = new Category();
            newCategory.CategoryId = Convert.ToInt32(reader["category_id"]);
            newCategory.CategoryName = Convert.ToString(reader["category_name"]);
            return newCategory;
        }
    }
}
