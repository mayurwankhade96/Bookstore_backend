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
    public class SellerRL : ISellerRL
    {
        private readonly string _connectionString;
        public SellerRL(IConfiguration config)
        {
            _connectionString = config.GetSection("ConnectionStrings").GetSection("Bookstore").Value;
        }

        private const string _insertQuery = "spSignupSeller";
        public bool RegisterSeller(Seller seller)
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
                    command.Parameters.AddWithValue("@full_name", seller.FullName);
                    command.Parameters.AddWithValue("@email", seller.Email);
                    command.Parameters.AddWithValue("@seller_password", seller.Password);
                    command.Parameters.AddWithValue("@mobile_number", seller.MobileNumber);
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
