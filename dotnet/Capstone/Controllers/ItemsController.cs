using Capstone.DAO.SqlDaoInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.Controllers
{
    [Route("[controller]")] // changed from items to [controller]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemDao itemDao;
        
        public ItemsController(IItemDao itemDao)
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
