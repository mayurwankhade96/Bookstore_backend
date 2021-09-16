using BusinessLayer.Inteface;
using CommonLayer.Models;
using RepositoryLayer.Inteface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        private IBookRL _bookRL;
        public BookBL(IBookRL bookRL)
        {
            this._bookRL = bookRL;
        }
        public bool AddBook(Book book)
        {
            try
            {
                return this._bookRL.AddBook(book);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Book> GetBooks()
        {
            try
            {
                return this._bookRL.GetBooks();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
