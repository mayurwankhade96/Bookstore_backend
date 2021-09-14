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
    public class CustomerRL : ICustomerRL
    {
        private readonly string _connectionString;
        public CustomerRL(IConfiguration config)
        {
            _connectionString = config.GetSection("ConnectionStrings").GetSection("Bookstore").Value;
        }

        private const string _insertQuery = "spSignupCustomer";
        public bool RegisterCustomer(Customer customer)
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

        private const string _selectQuery = "spCustomerLogin";
        public LoginResponse Login(string email, string password)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {             
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(_selectQuery, connection);
                    command.CommandType = CommandType.StoredProcedure;         
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@customer_password", password);
                    
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        LoginResponse login = new LoginResponse();
                        while (reader.Read())
                        {
                            login.Id = reader.GetInt32(0);
                            login.FullName = reader.GetString(1);
                            login.Email = reader.GetString(2);
                            login.MobileNumber = reader.GetString(3);
                        }
                        return login;
                    }
                    return null;
                }   
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
