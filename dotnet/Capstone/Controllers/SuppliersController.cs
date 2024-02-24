using Capstone.DAO.SqlDaoInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierDao supplierDao;

        public SuppliersController(ISupplierDao supplierDao)
        {
            this.supplierDao = supplierDao;
        }

        [HttpGet("/suppliers")]
        public ActionResult<List<Supplier>> GetSuppliers()
        {
            try
            {
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
