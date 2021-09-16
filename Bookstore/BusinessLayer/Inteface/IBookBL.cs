using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Inteface
{
    public interface IBookBL
    {
        bool AddBook(Book book);
        List<Book> GetBooks();
    }
}
