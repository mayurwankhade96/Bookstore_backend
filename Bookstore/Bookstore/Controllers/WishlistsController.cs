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
    public class WishlistsController : ControllerBase
    {
        private IWishlistBL _wishlistBL;
        public WishlistsController(IWishlistBL wishlistBL)
        {
            this._wishlistBL = wishlistBL;
        }

        [HttpPost]
        public ActionResult AddToWishlist(WishlistModel wishlist)
        {
            try
            {
                int customerId = Convert.ToInt32(User.FindFirst(x => x.Type == "userId").Value);

                var objectInWishlist = _wishlistBL.AddToWishlist(wishlist, customerId);

                if (objectInWishlist == true)
                {
                    return Ok(new { success = true, message = "**Added to wishlist successfully**", data = wishlist.BookId, wishlist.CustomerId });
                }
                return BadRequest(new { success = false, message = "Something is wrong!" });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpDelete]
        public ActionResult RemoveFromwishlist(int wishlistId)
        {
            try
            {
                int customerId = Convert.ToInt32(User.FindFirst(x => x.Type == "userId").Value);

                var bookToBeRemoved = _wishlistBL.RemoveFromWishlist(wishlistId, customerId);

                if (bookToBeRemoved == true)
                {
                    return Ok(new { message = "**Book removed from wishlist**" });
                }
                return BadRequest(new { message = "operation unsuccessfull -_-" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult GetWishlistDetails()
        {
            try
            {
                int customerId = Convert.ToInt32(User.FindFirst(x => x.Type == "userId").Value);

                var booksFromWishlist = _wishlistBL.GetWishlistDetails(customerId);

                if (booksFromWishlist != null)
                {
                    return Ok(new { message = "**Books are as follows**", data = booksFromWishlist });
                }
                return BadRequest(new { message = "there are no books in your wishlist..." });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
