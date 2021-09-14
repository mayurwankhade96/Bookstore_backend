using BusinessLayer.Inteface;
using CommonLayer.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerBL _customerBL;
        public CustomerController(ICustomerBL customerBL)
        {
            this._customerBL = customerBL;
        }

        [HttpPost]
        public ActionResult RegisterCustomer(AddCustomer customer)
        {
            try
            {
                var newCustomer = _customerBL.RegisterCustomer(customer);

                if(newCustomer == true)
                {
                    return Ok(new { success = true, message = "**Registered successfully**", customer.FullName, customer.Email  });
                }
                return BadRequest(new { success = false, message = "**Registration Failed**" });
            }
            catch(Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
