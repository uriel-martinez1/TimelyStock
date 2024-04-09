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
        
        public ItemsController(IItemDao itemDao, IUserDao userDao)
        {
            this.itemDao = itemDao;
            this.userDao = userDao;
        }

        [HttpGet()]
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

        [HttpPost()]
        public ActionResult<Item> AddItem(Item item)
        {
            try
            {
                Item added = itemDao.CreateItem(item);
                return Created($"/items/{added.ItemId}", added);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }
    }
}
