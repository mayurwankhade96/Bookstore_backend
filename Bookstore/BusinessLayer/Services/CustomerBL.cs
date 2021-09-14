using BusinessLayer.Inteface;
using CommonLayer.Response;
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
        public bool RegisterCustomer(AddCustomer customer)
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
