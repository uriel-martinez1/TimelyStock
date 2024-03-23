using Capstone.DAO.SqlDaoInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.Controllers
{
    [Route("suppliers")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierDao supplierDao;
        private readonly IUserDao userDao;

        public SuppliersController(ISupplierDao supplierDao, IUserDao userDao)
        {
            this.supplierDao = supplierDao;
            this.userDao = userDao;
        }

        [HttpGet()]
        public ActionResult<List<Supplier>> GetSuppliers()
        {
            try
            {
                User user = userDao.GetUserByUsername(User.Identity.Name);
                int userId = user.UserId;
                List<Supplier> outputList = supplierDao.GetSuppliers();
                return Ok(outputList);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }
    }
}
