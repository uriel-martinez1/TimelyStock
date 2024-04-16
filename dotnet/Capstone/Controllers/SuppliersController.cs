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
        public ActionResult<List<Supplier>> GetSuppliersByUserId()
        {
            try
            {
                User user = userDao.GetUserByUsername(User.Identity.Name);
                int userId = user.UserId;
                List<Supplier> outputList = supplierDao.GetSuppliersByUserId(userId);
                return Ok(outputList);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        [HttpPost()]
        public ActionResult<Supplier> AddSupplier(Supplier supplier)
        {
            try
            {
                User user = userDao.GetUserByUsername(User.Identity.Name);
                Supplier added = supplierDao.CreateSupplier(supplier, user);
                return Created($"/suppliers/{added.SupplierId}", added);

            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }
    }
}
