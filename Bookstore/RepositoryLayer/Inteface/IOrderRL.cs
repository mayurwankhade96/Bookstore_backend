using CommonLayer;
using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Inteface
{
    public interface IOrderRL
    {
        bool PlaceOrder(int customerId, int cartId);
        bool EmailOrderDetails(int customerId, int orderNumber);
        List<OrderResponse> GetOrders(int customerId);
    }
}
