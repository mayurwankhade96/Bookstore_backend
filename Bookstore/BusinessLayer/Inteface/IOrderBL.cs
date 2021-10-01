using CommonLayer;
using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Inteface
{
    public interface IOrderBL
    {
        bool PlaceOrder(int customerId, int cartId);       
        bool EmailOrderDetails(int customerId, int orderNumber);
        List<OrderResponse> GetOrders(int customerId);
    }
}
