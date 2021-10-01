using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class OrderResponse
    {
        public int OrderId { get; set; }
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string MobileNumber { get; set; }    
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string AuthorName { get; set; }
        public string BookImage { get; set; }
        public int AddressId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
