using Capstone.DAO.SqlDaoInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.Controllers
{
    [Route("items")] 
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemDao itemDao;
        private readonly IUserDao userDao;
        private readonly IInventoryDao inventoryDao;
        
        public ItemsController(IItemDao itemDao, IUserDao userDao, IInventoryDao inventoryDao)
        {
            this.itemDao = itemDao;
            this.userDao = userDao;
            this.inventoryDao = inventoryDao;
        }

        [HttpGet()]
        public ActionResult<List<Item>> GetItems()
        {
            try
            {
                User user = userDao.GetUserByUsername(User.Identity.Name);
                List<Item> outputList = itemDao.GetItemsByUserId(user.UserId);
                return Ok(outputList);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }
        // TODO -- Unsure whether we are going to need this? Test later!
        [HttpGet("/items/search")]
        public ActionResult<List<Item>> GetItemsByName(string name, bool useWildCard = false)
        {
            try
            {
                List<Item> items = itemDao.GetItemsByName(name, useWildCard);
                return Ok(items);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }
    }
}
