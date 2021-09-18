using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Inteface
{
    public interface ICartRL
    {
        bool AddToCart(CartModel cart, int cutomerId);
    }
}
