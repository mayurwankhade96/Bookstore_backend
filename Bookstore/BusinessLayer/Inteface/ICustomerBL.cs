using CommonLayer;
using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Inteface
{
    public interface ICustomerBL
    {
        bool RegisterCustomer(Customer customer);
        LoginResponse Login(string email, string password);
        bool ResetPassword(ResetPassword reset, int userId);
        string GenerateToken(string userEmail, int userId, string role);
    }
}
