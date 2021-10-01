using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class WishlistResponse
    {
        public int WishlistId { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string BookImage { get; set; }
    }
}
