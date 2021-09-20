using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class WishlistModel
    {
        public int WishlistId { get; set; }
        public int CustomerId { get; set; }
        public int BookId { get; set; }
    }
}
