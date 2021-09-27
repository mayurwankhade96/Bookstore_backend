using BusinessLayer.Inteface;
using CommonLayer;
using CommonLayer.Models;
using RepositoryLayer.Inteface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        private ICartRL _cartRL;
        public CartBL(ICartRL cartRL)
        {
            this._cartRL = cartRL;
        }

        public object AddToCart(CartModel cart, int cutomerId)
        {
            try
            {
                return this._cartRL.AddToCart(cart, cutomerId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteFromCart(int cartId, int customerId)
        {
            try
            {
                return this._cartRL.DeleteFromCart(cartId, customerId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CartResponse> GetCartDetails(int customerId)
        {
            try
            {
                return this._cartRL.GetCartDetails(customerId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
