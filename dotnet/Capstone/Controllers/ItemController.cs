using Capstone.DAO.SqlDaoInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.Controllers
{
    [Route("items")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemDao itemDao;
        
        public ItemController(IItemDao itemDao)
        {
            this.itemDao = itemDao;
        }

        [HttpGet("/items")]
        public ActionResult<List<Item>> GetItems()
        {
            try
            {
                List<Item> outputList = itemDao.GetItems();
                return Ok(outputList);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }
    }
}
