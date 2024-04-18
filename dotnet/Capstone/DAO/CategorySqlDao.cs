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

            string sql = "SELECT category_id, user_id, category_name FROM categories;";

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

        public List<Category> GetCategoriesByUserId(int userId)
        {
            List<Category> categories = new List<Category>();

            string sql = "SELECT DISTINCT category_id, user_id, category_name FROM categories " +
                "WHERE user_id = @userId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Category category = MapRowToCategory(reader);
                        categories.Add(category);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occured", ex);
            }
            return categories;
        }

        public Category GetCategoryByCategoryId(int categoryId)
        {
            Category category = new Category();

            string sql = "SELECT category_id,user_id, category_name " +
                "FROM categories " +
                "WHERE category_id = @categoryId;";

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@categoryId", categoryId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        category = MapRowToCategory(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL Exception occurred", ex);
            }
            return category;
        }

        public Category CreateCategory(Category category, User user)
        {
            Category newCategory = new Category();

            string sql = "INSERT INTO categories(user_id, category_name) " +
                "OUTPUT inserted.category_id " +
                "VALUES (@userId, @categoryName);";

            try
            {
                int newCategoryId;

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userId", user.UserId);
                    // TODO - lets add something that stops the creation of a category without a name
                    cmd.Parameters.AddWithValue("@categoryName", category.CategoryName);
                    newCategoryId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                newCategory = GetCategoryByCategoryId(newCategoryId);
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL Exception occured", ex);
            }
            return newCategory;
        }

        public Category MapRowToCategory(SqlDataReader reader)
        {
            Category newCategory = new Category();
            newCategory.CategoryId = Convert.ToInt32(reader["category_id"]);
            newCategory.UserId = Convert.ToInt32(reader["user_id"]);
            newCategory.CategoryName = Convert.ToString(reader["category_name"]);
            return newCategory;
        }
    }
}
