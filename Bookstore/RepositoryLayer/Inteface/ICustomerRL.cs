using CommonLayer;
using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Inteface
{
    public interface ICustomerRL
    {
        bool RegisterCustomer(Customer customer);
        LoginResponse Login(string email, string password);
        bool ResetPassword(ResetPassword reset, int userId);
    }
}
