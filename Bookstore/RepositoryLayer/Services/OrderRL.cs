using CommonLayer;
using CommonLayer.Models;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Inteface;
using RepositoryLayer.MSMQService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RepositoryLayer.Services
{
    public class OrderRL : IOrderRL
    {
        private readonly string _connectionString;
        public OrderRL(IConfiguration config)
        {
            _connectionString = config.GetSection("ConnectionStrings").GetSection("Bookstore").Value;
        }

        public bool EmailOrderDetails(int customerId, int orderNumber)
        {
            string email = GetEmailFromId(customerId);
            string messagecontent = "Your order has been successfully placed. your order number is  " + orderNumber;

            MessageQueue msgqueue;
            if (MessageQueue.Exists(@".\Private$\MyQueue"))
            {
                msgqueue = new MessageQueue(@".\Private$\MyQueue");
            }
            else
            {
                msgqueue = MessageQueue.Create(@".\Private$\MyQueue");
            }

            Message message = new Message();

            message.Formatter = new BinaryMessageFormatter();
            message.Body = messagecontent;
            msgqueue.Label = "your order number";
            msgqueue.Send(message);

            var receivequeue = new MessageQueue(@".\Private$\MyQueue");
            var receivemsg = receivequeue.Receive();
            receivemsg.Formatter = new BinaryMessageFormatter();

            string link = receivemsg.Body.ToString();
            if (Sendmail(email, link))
            {
                return true;
            }
            return false;
        }

        public bool PlaceOrder(int customerId, int cartId)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                int rows;
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spPlaceOrder", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@customer_id", customerId);
                    command.Parameters.AddWithValue("@cart_id", cartId);
                    rows = command.ExecuteNonQuery();

                    //SqlDataReader reader = command.ExecuteReader();
                    //if (reader.HasRows)
                    //{
                    //    OrderResponse orderDetails = new OrderResponse();
                    //    while (reader.Read())
                    //    {
                    //        orderDetails.OrderId = reader.GetInt32(0);
                    //        orderDetails.CartId = reader.GetInt32(1);
                    //        orderDetails.CustomerId = reader.GetInt32(2);
                    //        orderDetails.CustomerId = reader.GetInt32(3);
                    //        orderDetails.BookId = reader.GetInt32(4);
                    //        orderDetails.OrderPlaced = reader.GetBoolean(5);
                    //        orderDetails.OrderPlacedDate = reader.GetDateTime(6);
                    //    }
                    //    return orderDetails;
                    //}
                    //return null;
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

        private bool Sendmail(string email, string message)
        {
            MailMessage mailmessage = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            mailmessage.From = new MailAddress("mayur.wankhade2@gmail.com");
            mailmessage.To.Add(new MailAddress(email));
            mailmessage.Subject = "Your Order for book has been successfully placed";
            mailmessage.IsBodyHtml = true;
            mailmessage.Body = message;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new NetworkCredential("mayur.wankhade2@gmail.com", "khikhikhi");
            smtp.EnableSsl = true;
            smtp.Send(mailmessage);
            return true;
        }

        private string GetEmailFromId(int customerId)
        {
            string email = string.Empty;
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("spGetEmailFromId", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customer_id", customerId);
                    
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        email = reader["email"].ToString();
                    }
                    connection.Close();
                    return email;
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

        public List<OrderResponse> GetOrders(int customerId)
        {
            List<OrderResponse> orderResponses = new List<OrderResponse>();
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spGetOrders", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@customer_id", customerId);
                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        OrderResponse orders = new OrderResponse();
                        orders.OrderId = Convert.ToInt32(dataReader["order_id"]);
                        orders.BookId = Convert.ToInt32(dataReader["book_id"]);
                        orders.CartId = Convert.ToInt32(dataReader["cart_id"]);
                        orders.CustomerId = Convert.ToInt32(dataReader["customer_id"]);
                        orders.BookTitle = dataReader["book_title"].ToString();
                        orders.AuthorName = dataReader["author_name"].ToString();
                        orders.BookImage = dataReader["book_image"].ToString();                       
                        orderResponses.Add(orders);
                    }
                }
                return orderResponses;
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
