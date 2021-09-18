using BusinessLayer.Inteface;
using CommonLayer;
using CommonLayer.Models;
using RepositoryLayer.Inteface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddressBL : IAddressBL
    {
        private IAddressRL _addressRL;
        public AddressBL(IAddressRL addressRL)
        {
            this._addressRL = addressRL;
        }

        public bool AddNewAddress(int userId, AddressModel address)
        {
            try
            {
                return this._addressRL.AddNewAddress(userId, address);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AddressModel> GetAllAddress(int userId)
        {
            try
            {
                return this._addressRL.GetAllAddress(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateAddress(UpdateAddress update, int addressId, int customerId)
        {
            try
            {
                return this._addressRL.UpdateAddress(update, addressId, customerId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
