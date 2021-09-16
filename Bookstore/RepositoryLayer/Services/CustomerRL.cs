using CommonLayer;
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Inteface;
using RepositoryLayer.MSMQService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CustomerRL : ICustomerRL
    {
        private readonly string _connectionString;
        private readonly string _secret;
        public CustomerRL(IConfiguration config)
        {
            _connectionString = config.GetSection("ConnectionStrings").GetSection("Bookstore").Value;
            _secret = config.GetSection("AppSettings").GetSection("Key").Value;
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
                            login.Role = reader.GetString(4);
                        }
                        login.Token = GenerateToken(email, login.Id, login.Role);
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

        public string GenerateToken(string userEmail, int userId, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, userEmail),
                    new Claim("userId", userId.ToString(), ClaimValueTypes.Integer),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }

        private const string _updateQuery = "spResetPassword";
        public bool ResetPassword(ResetPassword reset, int userId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                int rows;
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(_updateQuery, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@customer_id", userId);
                    command.Parameters.AddWithValue("@customer_password", reset.NewPassword);
                    rows = command.ExecuteNonQuery();
                }
                return (rows > 0 ? true : false);
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

        private const string _forgetPasswordQuery = "spForgetPassword";
        public bool ForgetPassword(string email)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(_forgetPasswordQuery, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@email", email);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        Customer customer = new Customer();
                        while (reader.Read())
                        {
                            customer.CustomerId = reader.GetInt32(0);
                            customer.FullName = reader.GetString(1);
                            customer.Email = reader.GetString(2);
                            customer.Password = reader.GetString(3);
                            customer.MobileNumber = reader.GetString(4);
                            customer.Role = reader.GetString(5);
                        }
                        if(customer != null)
                        {
                            string token = GenerateToken(customer.Email, customer.CustomerId, customer.Role);
                            MSMQUtility mSMQ = new MSMQUtility();
                            mSMQ.SendMessage(email, token);

                            return true;
                        }
                        return false;
                    }
                    return false;
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
