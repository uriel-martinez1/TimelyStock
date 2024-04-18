using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO.SqlDaoInterfaces
{
    public interface ICategoryDao
    {
        List<Category> GetCategories();
        List<Category> GetCategoriesByUserId(int userId);
        Category CreateCategory(Category category, User user);
        Category GetCategoryByCategoryId(int categoryId);
    }
}
