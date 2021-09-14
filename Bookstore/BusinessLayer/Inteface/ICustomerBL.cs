using CommonLayer.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Inteface
{
    public interface ICustomerBL
    {
        bool RegisterCustomer(AddCustomer customer);
    }
}
