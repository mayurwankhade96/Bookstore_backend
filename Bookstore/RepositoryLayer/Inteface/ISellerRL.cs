using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Inteface
{
    public interface ISellerRL
    {
        bool RegisterSeller(Seller seller);
    }
}
