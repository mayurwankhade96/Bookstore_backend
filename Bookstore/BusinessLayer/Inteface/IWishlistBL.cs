using CommonLayer;
using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Inteface
{
    public interface IWishlistBL
    {
        bool AddToWishlist(WishlistModel wishlist, int cutomerId);
        bool RemoveFromWishlist(int wishlistId, int customerId);
        List<WishlistResponse> GetWishlistDetails(int customerId);
    }
}
