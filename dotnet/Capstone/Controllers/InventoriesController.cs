using Capstone.DAO.SqlDaoInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Capstone.Models;
using System;

namespace Capstone.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly IInventoryDao inventoryDao;
        private readonly IUserDao userDao;

        public InventoriesController(IInventoryDao inventoryDao, IUserDao userDao)
        {
            this.inventoryDao = inventoryDao;
            this.userDao = userDao;
        }

        [HttpGet()]
        public ActionResult<List<Inventory>> GetInventories()
        {
            try
            {
                List<Inventory> outputList = inventoryDao.GetInventories();
                return Ok(outputList);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        [HttpGet("{userId}")]
        public ActionResult<List<Inventory>> GetUserInventories(int userId)
        {
            try
            {
                List<Inventory> outputList = inventoryDao.GetInventoriesByUserId(userId);
                return Ok(outputList);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        //[HttpPost()]
        //public ActionResult<Inventory> AddInventory(Inventory inventory)
        //{
        //    User user = userDao.GetUserByUsername(User.Identity.Name);
        //    Console.WriteLine(User.Identity.Name);
        //    Inventory added = inventoryDao.CreateInventory(inventory, user);
        //    return Created($"/inventories/{added.InventoryId}", added);
        //}


        [HttpPost()]
        public ActionResult<Inventory> AddInventory(Inventory inventory)
        {
            Inventory added = inventoryDao.CreateInventory(inventory);
            return Created($"/inventories/{added.InventoryId}", added);
        }

        [HttpPut("{id}")]
        public ActionResult<Inventory> UpdateInventory(int id, Inventory inventoryToUpdate)
        {
            if(id != inventoryToUpdate.InventoryId && inventoryToUpdate.InventoryId <= 0)
            {
                return BadRequest();
            }

            try
            {
                Inventory updatedInventory = inventoryDao.UpdateInventory(inventoryToUpdate);
                return Ok(updatedInventory);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
