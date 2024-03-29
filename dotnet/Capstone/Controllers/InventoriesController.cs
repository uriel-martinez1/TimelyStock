﻿using Capstone.DAO.SqlDaoInterfaces;
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
    }
}
