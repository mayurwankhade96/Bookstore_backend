using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Inteface
{
    public interface ISellerBL
    {
        bool RegisterSeller(Seller seller);
    }
}
