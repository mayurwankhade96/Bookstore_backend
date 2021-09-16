using BusinessLayer.Inteface;
using CommonLayer;
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Inteface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer.Services
{
    public class CustomerBL : ICustomerBL
    {
        private ICustomerRL _customerRL;
        private readonly string _secret;
        public CustomerBL(ICustomerRL customerRL, IConfiguration config)
        {
            this._customerRL = customerRL;
            _secret = config.GetSection("AppSettings").GetSection("Key").Value;
        }

        public string GenerateToken(string userEmail, int userId, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, userEmail),
                    new Claim("userId", userId.ToString(), ClaimValueTypes.Integer),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string jwtToken = tokenHandler.WriteToken(token);
            return jwtToken;
        }

        public LoginResponse Login(string email, string password)
        {
            try
            {
                return this._customerRL.Login(email, password);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RegisterCustomer(Customer customer)
        {
            try
            {
                return this._customerRL.RegisterCustomer(customer);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public bool ResetPassword(ResetPassword reset, int userId)
        {
            try
            {
                return this._customerRL.ResetPassword(reset, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }       
    }
}
