﻿using DataAccess.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinimalApi
{
    public class TokenService(IConfiguration configuration):ITokenService
    {
        private IConfiguration _configuration = configuration;

        public string BuildToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,value: user.Nombre),
            };
            var token = new JwtSecurityToken(_configuration["Auth:Jwt:Issuer"], _configuration["Auth:Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Auth:Jwt:TokenExpirationInMinutes"])), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
