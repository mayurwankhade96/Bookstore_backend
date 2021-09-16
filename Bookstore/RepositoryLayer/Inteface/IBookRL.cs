using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Inteface
{
    public interface IBookRL
    {
        bool AddBook(Book book);
        List<Book> GetBooks();
    }
}
