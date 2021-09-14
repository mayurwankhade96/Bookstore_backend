using BusinessLayer.Inteface;
using CommonLayer.Models;
using RepositoryLayer.Inteface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class SellerBL : ISellerBL
    {
        private ISellerRL _sellerRL;
        public SellerBL(ISellerRL _sellerRL)
        {
            this._sellerRL = _sellerRL;
        }
        public bool RegisterSeller(Seller seller)
        {
            try
            {
                return this._sellerRL.RegisterSeller(seller);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
