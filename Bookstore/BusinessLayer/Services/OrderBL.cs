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

        public List<OrderResponse> GetOrders(int customerId)
        {
            try
            {
                return this._orderRL.GetOrders(customerId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PlaceOrder(int customerId, int cartId)
        {
            try
            {
                return this._orderRL.PlaceOrder(customerId, cartId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
