using CommonLayer;
using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Inteface
{
    public interface ICartBL
    {
        bool AddToCart(CartModel cart, int cutomerId);
        bool DeleteFromCart(int cartId, int customerId);
    }
}
