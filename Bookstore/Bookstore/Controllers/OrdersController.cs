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
    public class OrdersController : ControllerBase
    {
        private IOrderBL _orderBL;
        public OrdersController(IOrderBL orderBL)
        {
            this._orderBL = orderBL;
        }

        [HttpPost]
        public ActionResult PlaceOrder(OrderModel order)
        {
            try
            {
                int customerId = Convert.ToInt32(User.FindFirst(x => x.Type == "userId").Value);

                var orderToBePlaced = this._orderBL.PlaceOrder(customerId, order.CartId);

                if (orderToBePlaced == true)
                {
                    //_orderBL.EmailOrderDetails(customerId, order.OrderId);
                    return Ok(new { success = true, message = "**Order Placed Successfully**" });
                }
                return BadRequest(new { success = false, message = "Something is wrong!" });
            }                      
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult GetOrders()
        {
            try
            {
                int customerId = Convert.ToInt32(User.FindFirst(x => x.Type == "userId").Value);

                var allOrders = this._orderBL.GetOrders(customerId);

                if(allOrders != null)
                {
                    return Ok(new { success = true, message = "**Order are as follows**", data = allOrders });
                }
                return BadRequest(new { success = false, message = "There are no orders" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
