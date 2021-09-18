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
    public class AddressRL : IAddressRL
    {
        private readonly string _connectionString;
        public AddressRL(IConfiguration config)
        {
            _connectionString = config.GetSection("ConnectionStrings").GetSection("Bookstore").Value;
        }

        private const string _insertQuery = "spAddAddress";
        public bool AddNewAddress(int userId, AddressModel address)
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
                    command.Parameters.AddWithValue("@address", address.Address);
                    command.Parameters.AddWithValue("@city", address.City);
                    command.Parameters.AddWithValue("@state", address.State);
                    command.Parameters.AddWithValue("@type_of", address.TypeOf);
                    command.Parameters.AddWithValue("@customer_id", userId);
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

        private const string _selectQuery = "spGetAddresses";
        public List<AddressModel> GetAllAddress(int userId)
        {
            List<AddressModel> addresses = new List<AddressModel>();
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(_selectQuery, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@customer_id", userId);
                SqlDataReader sqlreader = command.ExecuteReader();
                while (sqlreader.Read())
                {
                    AddressModel address1 = new AddressModel();
                    address1.AddressId = Convert.ToInt32(sqlreader["address_id"]);
                    address1.Address = sqlreader["address"].ToString();
                    address1.City = sqlreader["city"].ToString();
                    address1.State = sqlreader["state"].ToString();
                    address1.TypeOf = sqlreader["type_of"].ToString();
                    addresses.Add(address1);
                }
                return addresses;
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

        public bool UpdateAddress(UpdateAddress update, int addressId, int customerId)
        {
            int rows;
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spUpdateAddress", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@address", update.Address);
                    command.Parameters.AddWithValue("@city", update.City);
                    command.Parameters.AddWithValue("@state", update.State);
                    command.Parameters.AddWithValue("@type_of", update.TypeOf);
                    command.Parameters.AddWithValue("@address_id", addressId);
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
