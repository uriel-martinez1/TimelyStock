using Capstone.DAO.SqlDaoInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.Controllers
{
    [Route("categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryDao categoryDao;
        private readonly IUserDao userDao;

        public CategoriesController(ICategoryDao categoryDao, IUserDao userDao)
        {
            this.categoryDao = categoryDao;
            this.userDao = userDao;
        }

        [HttpGet()]
        public ActionResult<List<Category>> GetCategories()
        {
            try
            {
                User user = userDao.GetUserByUsername(User.Identity.Name);
                int userId = user.UserId;
                List<Category> outputList = categoryDao.GetCategoriesByUserId(userId);
                return Ok(outputList);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        [HttpPost()]
        public ActionResult<Category> AddCategory(Category category)
        {
            try
            {
                User user = userDao.GetUserByUsername(User.Identity.Name);
                Category newCategory = categoryDao.CreateCategory(category, user);
                return Created($"/categories/{newCategory.CategoryId}", newCategory);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }
    }
}
