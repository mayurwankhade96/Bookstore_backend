using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
