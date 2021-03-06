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

                if (objectInCart != null)
                {
                    return Ok(new { success = true, message = "**Added to cart successfully**", cart.BookId, customerId });
                }
                return BadRequest(new { success = false, message = "Something is wrong!" });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpDelete]
        public ActionResult DeleteFromCart(int cartId)
        {
            try
            {
                int customerId = Convert.ToInt32(User.FindFirst(x => x.Type == "userId").Value);

                var bookToBeDeleted = _cartBL.DeleteFromCart(cartId, customerId);

                if (bookToBeDeleted == true)
                {
                    return Ok(new { message = "**Book removed from cart**" });
                }
                return BadRequest(new { message = "operation unsuccessfull -_-" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult GetCartDetails()
        {
            try
            {
                int customerId = Convert.ToInt32(User.FindFirst(x => x.Type == "userId").Value);

                var booksFromCart = _cartBL.GetCartDetails(customerId);

                if (booksFromCart != null)
                {
                    return Ok(new { message = "**Books are as follows**", data = booksFromCart });
                }
                return BadRequest(new { message = "there are no books in your cart..." });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
