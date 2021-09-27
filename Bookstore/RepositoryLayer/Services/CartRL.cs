using CommonLayer;
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
    public class CartRL : ICartRL
    {
        private readonly string _connectionString;
        public CartRL(IConfiguration config)
        {
            _connectionString = config.GetSection("ConnectionStrings").GetSection("Bookstore").Value;
        }

        public object AddToCart(CartModel cart, int cutomerId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                int rows;
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spAddToCart", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@customer_id", cutomerId);
                    command.Parameters.AddWithValue("@book_id", cart.BookId);
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

        public bool DeleteFromCart(int cartId, int customerId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                int rows;
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spDeleteFromCart", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@cart_id", cartId);
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

        public List<CartResponse> GetCartDetails(int customerId)
        {
            List<CartResponse> booksInCart = new List<CartResponse>();
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spGetBooksFromCart", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@customer_id", customerId);
                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        CartResponse cart = new CartResponse();
                        cart.BookId = Convert.ToInt32(dataReader["book_id"]);
                        cart.CartId = Convert.ToInt32(dataReader["cart_id"]);
                        cart.BookTitle = dataReader["book_title"].ToString();
                        cart.AuthorName = dataReader["author_name"].ToString();
                        cart.Description = dataReader["book_description"].ToString();                       
                        cart.Price = Convert.ToInt32(dataReader["book_price"]);
                        cart.BookImage = dataReader["book_image"].ToString();
                        booksInCart.Add(cart);
                    }                    
                }
                return booksInCart;
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
    }
}
