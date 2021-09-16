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
    public class BooksController : ControllerBase
    {
        private IBookBL _bookBL;
        public BooksController(IBookBL bookBL)
        {
            this._bookBL = bookBL;
        }

        [HttpPost]
        public ActionResult RegisterCustomer(Book book)
        {
            try
            {
                var newBook = _bookBL.AddBook(book);

                if (newBook == true)
                {
                    return Ok(new { success = true, message = "**Book added successfully**", data = book });
                }
                return BadRequest(new { success = false, message = "Book Addition Failed! Please try again..." });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult GetBooks()
        {
            try
            {
                var books = _bookBL.GetBooks();

                if (books.Count != 0)
                {
                    return Ok(new { success = true, message = "Books are as follows : ", data = books });
                }
                return BadRequest(new { success = false, message = "Failed to show books" });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
