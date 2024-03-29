﻿using Capstone.DAO.SqlDaoInterfaces;
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
        
        public ItemsController(IItemDao itemDao)
        {
            this.itemDao = itemDao;
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
    }
}
