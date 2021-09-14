using CommonLayer.Models;
using CommonLayer.Response;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Inteface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CustomerRL : ICustomerRL
    {
        private readonly string _connectionString;
        public CustomerRL(IConfiguration config)
        {
            _connectionString = config.GetSection("ConnectionStrings").GetSection("Bookstore").Value;
        }

        private const string _insertQuery = "spSignupCustomer";
        public bool RegisterCustomer(AddCustomer customer)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                Customer cust = new Customer()
                {
                    FullName = customer.FullName,
                    Email = customer.Email,
                    Password = customer.Password,
                    MobileNumber = customer.MobileNumber
                };
                
                int rows;
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(_insertQuery, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@full_name", customer.FullName);
                    command.Parameters.AddWithValue("@email", customer.Email);
                    command.Parameters.AddWithValue("@customer_password", customer.Password);
                    command.Parameters.AddWithValue("@mobile_number", customer.MobileNumber);
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
