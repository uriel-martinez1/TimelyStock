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
        // we are going to need to send the inventory id from the route here
        public ActionResult<Item> AddItem(Item item, int inventoryId)
        {
            try
            {
                // first we create the item
                Item added = itemDao.CreateItem(item);
                // then we need to link the new item to the inventory it is in
                bool linkSuccess = itemDao.LinkItemInventory(inventoryId, added.ItemId);
                // then we return created
                if (linkSuccess)
                {
                    // if successful, return created
                    return Created($"/items/{added.ItemId}", added);
                }
                else
                {
                    // lets return something if the item does not properly link
                    return BadRequest("Failed to link item to inventory.");
                }
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }
    }
}
