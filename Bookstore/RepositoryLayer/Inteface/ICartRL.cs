using CommonLayer;
using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Inteface
{
    public interface ICartRL
    {
        object AddToCart(CartModel cart, int cutomerId);
        bool DeleteFromCart(int cartId, int customerId);
        List<CartResponse> GetCartDetails(int customerId);
    }
}
