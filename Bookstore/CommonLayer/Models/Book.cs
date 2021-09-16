using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class Book
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public string BookTitle { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public int BookPrice { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string BookImage { get; set; }
        public double Rating { get; set; }
    }
}
