using BusinessLayer.Inteface;
using CommonLayer.Models;
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
        public ActionResult RegisterCustomer(Customer customer)
        {
            try
            {
                var newCustomer = _customerBL.RegisterCustomer(customer);

                if(newCustomer == true)
                {
                    return Ok(new { success = true, message = "**Registered successfully**", customer.FullName, customer.Email  });
                }
                return BadRequest(new { success = false, message = "Registration Failed! Please try again..." });
            }
            catch(Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("login")]
        public ActionResult CustomerLogin(Login login)
        {
            try
            {
                var existingCustomer = _customerBL.Login(login.Email, login.Password);

                if (existingCustomer != null)
                {
                    return Ok(new { success = true, message = "**Login successfull**", data = existingCustomer });
                }
                return BadRequest(new { success = false, message = "Login Failed! Please try again..." });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
