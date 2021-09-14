using CommonLayer.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Inteface
{
    public interface ICustomerRL
    {
        bool RegisterCustomer(AddCustomer customer);
    }
}
