using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Inteface
{
    public interface IAddressBL
    {
        bool AddNewAddress(int userId, AddressModel address);
        List<AddressModel> GetAllAddress(int userId);
    }
}
