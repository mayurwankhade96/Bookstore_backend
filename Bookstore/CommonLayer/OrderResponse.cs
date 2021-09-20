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
        public int AddressId { get; set; }
        public int BookId { get; set; }                            
        public bool OrderPlaced { get; set; }
        public DateTime OrderPlacedDate { get; set; }      
    }
}
