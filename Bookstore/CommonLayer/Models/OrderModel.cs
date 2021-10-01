using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class OrderModel
    {
        [Required]
        public int CartId { get; set; }
    }
}
