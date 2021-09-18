using BusinessLayer.Inteface;
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

        public bool AddToCart(CartModel cart, int cutomerId)
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
    }
}
