using CommonLayer;
using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Inteface
{
    public interface ICartBL
    {
        object AddToCart(CartModel cart, int cutomerId);
        bool DeleteFromCart(int cartId, int customerId);
        List<CartResponse> GetCartDetails(int customerId);
    }
}
