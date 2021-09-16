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
    public class SellerController : ControllerBase
    {
        private ISellerBL _sellerBL;
        public SellerController(ISellerBL sellerBL)
        {
            this._sellerBL = sellerBL;
        }

        [HttpPost]
        public ActionResult RegisterCustomer(Seller seller)
        {
            try
            {
                var newSeller = _sellerBL.RegisterSeller(seller);

                if (newSeller == true)
                {
                    return Ok(new { success = true, message = "**Registered successfully**", seller.FullName, seller.Email });
                }
                return BadRequest(new { success = false, message = "Registration Failed! Please try again..." });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("login")]
        public ActionResult SellerLogin(Login login)
        {
            try
            {
                var existingSeller = _sellerBL.Login(login.Email, login.Password);

                if (existingSeller != null)
                {
                    return Ok(new { success = true, message = "**Login successfull**", data = existingSeller });
                }
                return BadRequest(new { success = false, message = "Incorrect email or password! Please try again..." });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
