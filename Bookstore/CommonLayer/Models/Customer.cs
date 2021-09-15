using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9]+\.[a-zA-Z0-9-.]+$", ErrorMessage ="Invalid Email Format")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string Role { get; set; }
    }
}
