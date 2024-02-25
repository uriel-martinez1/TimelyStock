using Capstone.DAO.SqlDaoInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryDao categoryDao;

        public CategoriesController(ICategoryDao categoryDao)
        {
            this.categoryDao = categoryDao;
        }

        [HttpGet("/categories")]
        public ActionResult<List<Category>> GetCategories()
        {
            try
            {
                List<Category> outputList = categoryDao.GetCategories();
                return Ok(outputList);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }
    }
}
