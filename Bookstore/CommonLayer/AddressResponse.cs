using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class AddressResponse
    {
        public int AddressId { get; set; }
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string TypeOf { get; set; }
    }
}
