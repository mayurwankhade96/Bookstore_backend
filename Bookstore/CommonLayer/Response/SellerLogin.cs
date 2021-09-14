using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Response
{
    public class SellerLogin
    {
        public int SellerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
