using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Inteface
{
    public interface IWishlistRL
    {
        bool AddToWishlist(WishlistModel wishlist, int cutomerId);
        bool RemoveFromWishlist(int wishlistId, int customerId);
    }
}
