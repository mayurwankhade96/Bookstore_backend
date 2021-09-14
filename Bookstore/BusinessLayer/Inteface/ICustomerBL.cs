﻿using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Inteface
{
    public interface ICustomerBL
    {
        bool RegisterCustomer(Customer customer);
        LoginResponse Login(string email, string password);
    }
}
