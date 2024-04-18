using Capstone.DAO.SqlDaoInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Capstone.Models;
using System;
using Capstone.Exceptions;

namespace Capstone.Controllers
{
    [Route("inventories")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly IInventoryDao inventoryDao;
        private readonly IUserDao userDao;
        private readonly IItemDao itemDao;

        public InventoriesController(IInventoryDao inventoryDao, IUserDao userDao, IItemDao itemDao)
        {
            this.inventoryDao = inventoryDao;
            this.userDao = userDao;
            this.itemDao = itemDao;
        }

        [HttpGet()]
        public ActionResult<List<Inventory>> GetInventories()
        {
            try
            {
                User user = userDao.GetUserByUsername(User.Identity.Name);
                int userId = user.UserId;
                List<Inventory> outputList = inventoryDao.GetInventoriesByUserId(userId);
                return Ok(outputList);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{inventoryId}")]
        public ActionResult<Inventory> GetInventoryByInventoryId(int inventoryId)
        {
            try
            {
                Inventory output = inventoryDao.GetInventoryById(inventoryId);
                return Ok(output);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        [HttpPost()]
        public ActionResult<Inventory> AddInventory(Inventory inventory)
        {
            try
            {
                User user = userDao.GetUserByUsername(User.Identity.Name);
                Inventory added = inventoryDao.CreateInventory(inventory, user);
                return Created($"/inventories/{added.InventoryId}", added);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        [HttpPut("{inventoryId}")]
        public ActionResult<Inventory> UpdateInventory(int inventoryId, Inventory inventoryToUpdate)
        {
            try
            {
                Inventory updatedInventory = inventoryDao.UpdateInventory(inventoryId,inventoryToUpdate);
                return Ok(updatedInventory);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{inventoryId}")]
        public ActionResult DeleteInventory(int inventoryId)
        {
            try
            {
                int confirmDelete = inventoryDao.DeleteInventory(inventoryId);
                return confirmDelete != 0 ? NoContent() : NotFound();
            }
            catch (System.Exception)
            {
                return BadRequest("Does that inventory exist?");
            }
        }

        [HttpGet("{inventoryId}/item")]
        public ActionResult<List<Item>> GetItemsByInventoryId(int inventoryId)
        {
            try
            {
                List<Item> outputList = itemDao.GetItemsByInventoryId(inventoryId);
                return Ok(outputList);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        // still unsure but trying to following good RESTful pattern
        // create item based on inventoryID

        [HttpPost("{inventoryId}/item")]
        public ActionResult<Item> AddItemToInventory(int inventoryId, Item item)
        {
            try
            {
                // Create the item
                Item newItem = itemDao.CreateItem(item);

                // Link the new item to the inventory
                bool linkSuccess = itemDao.LinkItemInventory(inventoryId, newItem.ItemId);

                // check if linking is successful
                if (linkSuccess)
                {
                    // if successful return created 
                    return Created($"/inventories/{inventoryId}/items/{newItem.ItemId}", newItem);
                }
                else
                {
                    // if not successful, return something that indicates that something went wrong
                    return BadRequest("Failed to link item to inventory.");
                }
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        // Update item based on inventoryId and itemId

        [HttpPut("{inventoryId}/item/{itemid}")]
        public ActionResult<Item> UpdateItemFromInventory(int inventoryId, int itemId, Item itemToUpdate)
        {
            try
            {
                // we need to update the item
                Item updatedItem = itemDao.UpdateItem(itemId, itemToUpdate);
                bool ifMatch = itemDao.ConfirmUpdatedItemFoundInInventory(inventoryId, updatedItem);
                // return ok if successful
                if (ifMatch)
                {
                    return Ok(updatedItem);
                }
                else
                {
                    // If the item is not found in the correct inventory, return NotFound
                    return NotFound("item not found in the specified inventory");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating item from inventory.");
            }
        }

        [HttpDelete("{inventoryId}/item/{itemid}")]
        public ActionResult DeleteItemFromInventory(int inventoryId, int itemId)
        {
            try
            {
                int confirmDelete = itemDao.DeleteItemByInventoryIdAndItemId(inventoryId, itemId);
                return confirmDelete != 0 ? NoContent() : NotFound();
            }
            catch (System.Exception)
            {
                return BadRequest("Does that inventory exist?");
            }
        }
    }
}
