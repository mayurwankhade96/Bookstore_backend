using BusinessLayer.Inteface;
using CommonLayer;
using CommonLayer.Models;
using RepositoryLayer.Inteface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishlistBL : IWishlistBL
    {
        private IWishlistRL _wishlistRL;
        public WishlistBL(IWishlistRL wishlistRL)
        {
            this._wishlistRL = wishlistRL;
        }
        public bool AddToWishlist(WishlistModel wishlist, int cutomerId)
        {
            try
            {
                return this._wishlistRL.AddToWishlist(wishlist, cutomerId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<WishlistResponse> GetWishlistDetails(int customerId)
        {
            try
            {
                return this._wishlistRL.GetWishlistDetails(customerId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveFromWishlist(int wishlistId, int customerId)
        {
            try
            {
                return this._wishlistRL.RemoveFromWishlist(wishlistId, customerId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
