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

        public bool AddToCart(CartModel cart, int cutomerId)
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
    }
}
