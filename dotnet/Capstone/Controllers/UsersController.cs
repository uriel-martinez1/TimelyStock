using Capstone.DAO.SqlDaoInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserDao userDao;

        public UsersController(IUserDao userDao)
        {
            this.userDao = userDao;
        }

        [HttpGet("/users")]
        public ActionResult<List<User>> GetUsers()
        {
            try
            {
                List<User> outputList = userDao.GetUsers();
                return Ok(outputList);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }
    }
}
