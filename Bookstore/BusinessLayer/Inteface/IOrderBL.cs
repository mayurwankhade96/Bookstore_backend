using CommonLayer;
using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Inteface
{
    public interface IOrderBL
    {
        bool PlaceOrder(OrderModel order, int customerId);
        bool EmailOrderDetails(int customerId, int orderNumber);
    }
}
