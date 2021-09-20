using BusinessLayer.Inteface;
using CommonLayer;
using CommonLayer.Models;
using RepositoryLayer.Inteface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrderBL : IOrderBL
    {
        private IOrderRL _orderRL;
        public OrderBL(IOrderRL orderRL)
        {
            this._orderRL = orderRL;
        }

        public bool EmailOrderDetails(int customerId, int orderNumber)
        {
            try
            {
                return this._orderRL.EmailOrderDetails(customerId, orderNumber);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PlaceOrder(OrderModel order, int customerId)
        {
            try
            {
                return this._orderRL.PlaceOrder(order, customerId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
