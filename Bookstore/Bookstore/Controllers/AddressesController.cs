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
    public class AddressesController : ControllerBase
    {
        private IAddressBL _addressBL;
        public AddressesController(IAddressBL addressBL)
        {
            this._addressBL = addressBL;
        }

        private int GetIdFromToken()
        {
            return Convert.ToInt32(User.FindFirst(x => x.Type == "userId").Value);
        }

        [HttpPost]
        public ActionResult AddNewAddress(AddressModel address)
        {
            try
            {
                int userId = GetIdFromToken();

                var newAddress = _addressBL.AddNewAddress(userId, address);

                if (newAddress == true)
                {
                    return Ok(new { success = true, message = "**Address added successfully**", data = address });
                }
                return BadRequest(new { success = false, message = "Please Fill Details properly!" });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult GetAddresses()
        {
            try
            {
                int userId = GetIdFromToken();
                var addresses = _addressBL.GetAllAddress(userId);

                if (addresses.Count != 0)
                {
                    return Ok(new { success = true, message = "Addresses are as follows : ", data = addresses});
                }
                return BadRequest(new { success = false, message = "There are no addresses" });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpPut]
        public ActionResult UpdateAddress(UpdateAddress update, int addressId)
        {
            try
            {
                int customerId = GetIdFromToken();

                var noteToBeUpdated = _addressBL.UpdateAddress(update, addressId, customerId);

                if (noteToBeUpdated == true)
                {                   
                    return Ok(new { message = "**Address Updated Successfully**", data = update });
                }
                return BadRequest(new { message = "operation unsuccessfull -_-" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
