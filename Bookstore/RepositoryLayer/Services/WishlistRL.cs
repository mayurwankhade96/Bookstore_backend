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
    public class WishlistRL : IWishlistRL
    {
        private readonly string _connectionString;
        public WishlistRL(IConfiguration config)
        {
            _connectionString = config.GetSection("ConnectionStrings").GetSection("Bookstore").Value;
        }

        public bool AddToWishlist(WishlistModel wishlist, int cutomerId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                int rows;
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spAddToWishlist", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@customer_id", cutomerId);
                    command.Parameters.AddWithValue("@book_id", wishlist.BookId);
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

        public bool RemoveFromWishlist(int wishlistId, int customerId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                int rows;
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spRemoveFromWishlist", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@wishlist_id", wishlistId);
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

        public List<WishlistResponse> GetWishlistDetails(int customerId)
        {
            List<WishlistResponse> booksInWishlist = new List<WishlistResponse>();
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spGetBooksFromWishlist", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@customer_id", customerId);
                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        WishlistResponse wishlist = new WishlistResponse();
                        wishlist.BookId = Convert.ToInt32(dataReader["book_id"]);
                        wishlist.WishlistId = Convert.ToInt32(dataReader["wishlist_id"]);
                        wishlist.BookTitle = dataReader["book_title"].ToString();
                        wishlist.AuthorName = dataReader["author_name"].ToString();
                        wishlist.Description = dataReader["book_description"].ToString();
                        wishlist.Price = Convert.ToInt32(dataReader["book_price"]);
                        wishlist.BookImage = dataReader["book_image"].ToString();
                        booksInWishlist.Add(wishlist);
                    }
                }
                return booksInWishlist;
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
