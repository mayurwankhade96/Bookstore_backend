using BusinessLayer.Inteface;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "users")]
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private ICartBL _cartBL;
        public CartsController(ICartBL cartBL)
        {
            this._cartBL = cartBL;
        }

        [HttpPost]
        public ActionResult AddToCart(CartModel cart)
        {
            try
            {
                int customerId = Convert.ToInt32(User.FindFirst(x => x.Type == "userId").Value);

                var objectInCart = _cartBL.AddToCart(cart, customerId);

                if (objectInCart == true)
                {
                    return Ok(new { success = true, message = "**Added to cart successfully**", data = cart });
                }
                return BadRequest(new { success = false, message = "Something is wrong!" });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
