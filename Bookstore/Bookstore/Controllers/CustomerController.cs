using BusinessLayer.Inteface;
using CommonLayer;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
                    return Ok(new { success = true, message = "**Registered successfully**", data = customer.Email, customer.FullName, customer.Role });
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
                return BadRequest(new { success = false, message = "Incorrect email or password! Please try again..." });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        private int GetIdFromToken()
        {
            return Convert.ToInt32(User.FindFirst(x => x.Type == "userId").Value);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("resetpassword")]
        public ActionResult ResetPassword(ResetPassword reset)
        {
            try
            {
                int customerId = GetIdFromToken();

                var existingCustomer = _customerBL.ResetPassword(reset, customerId);

                if(existingCustomer == true)
                {
                    return Ok(new { message = "Password reset successful" });
                }
                return BadRequest(new { success = false, message = "Password reset failed" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("forgetpassword")]
        public ActionResult ForgetPassword([FromBody] ForgetPassword fogetPassword)
        {
            try
            {
                var passwordForgottenUser = _customerBL.ForgetPassword(fogetPassword.Email);

                if(passwordForgottenUser == true)
                {
                    return Ok(new { message = "Link has been sent to given email id..." });
                }
                return BadRequest(new { success = false, message = "Invalid email address!" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
