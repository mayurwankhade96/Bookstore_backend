using CommonLayer;
using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Inteface
{
    public interface IAddressRL
    {
        bool AddNewAddress(int userId, AddressModel address);
        List<AddressModel> GetAllAddress(int userId);
        bool UpdateAddress(UpdateAddress update, int addressId, int customerId);
    }
}
