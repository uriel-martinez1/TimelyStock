using Capstone.DAO.SqlDaoInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly IInventoryDao inventoryDao;

        public InventoriesController(IInventoryDao inventoryDao)
        {
            this.inventoryDao = inventoryDao;
        }

        [HttpGet("/inventories")]
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
    }
}
