using BusinessLayer.Inteface;
using CommonLayer.Models;
using RepositoryLayer.Inteface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CustomerBL : ICustomerBL
    {
        private ICustomerRL _customerRL;
        public CustomerBL(ICustomerRL customerRL)
        {
            this._customerRL = customerRL;
        }

        public LoginResponse Login(string email, string password)
        {
            try
            {
                return this._customerRL.Login(email, password);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RegisterCustomer(Customer customer)
        {
            try
            {
                return this._customerRL.RegisterCustomer(customer);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
