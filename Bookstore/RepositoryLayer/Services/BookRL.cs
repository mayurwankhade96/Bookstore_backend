using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Inteface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookRL : IBookRL
    {
        private readonly string _connectionString;
        public BookRL(IConfiguration config)
        {
            _connectionString = config.GetSection("ConnectionStrings").GetSection("Bookstore").Value;
        }
        private const string _insertQuery = "spAddBook";
        public bool AddBook(Book book)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                int rows;
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(_insertQuery, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@book_title", book.BookTitle);
                    command.Parameters.AddWithValue("@author_name", book.AuthorName);
                    command.Parameters.AddWithValue("@book_price", book.BookPrice);
                    command.Parameters.AddWithValue("@book_description", book.Description);
                    command.Parameters.AddWithValue("@quantity", book.Quantity);
                    command.Parameters.AddWithValue("@book_image", book.BookImage);
                    command.Parameters.AddWithValue("@rating", book.Rating);
                    rows = command.ExecuteNonQuery();
                }
                return (rows > 0 ? true : false);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        private const string _selectQuery = "spGetBooks";
        public List<Book> GetBooks()
        {
            List<Book> bookList = new List<Book>();
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(_selectQuery, connection);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader sqlreader = command.ExecuteReader();
                while (sqlreader.Read())
                {
                    Book book = new Book();
                    book.BookId = Convert.ToInt32(sqlreader["book_id"]);
                    book.BookTitle = sqlreader["book_title"].ToString();
                    book.AuthorName = sqlreader["author_name"].ToString();
                    book.BookPrice = Convert.ToInt32(sqlreader["book_price"]);
                    book.Quantity = Convert.ToInt32(sqlreader["quantity"]);
                    book.Description = sqlreader["book_description"].ToString();
                    book.BookImage = sqlreader["book_Image"].ToString();
                    book.Rating = Convert.ToDouble(sqlreader["rating"]);
                    bookList.Add(book);
                }                                
                return bookList;
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
