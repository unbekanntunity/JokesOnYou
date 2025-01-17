﻿using JokesOnYou.Web.Api.Models;
using JokesOnYou.Web.Api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JokesOnYou.Web.Api.Services
{
    public class JwtTokenService : ITokenService
    {
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly byte[] _key;

        public JwtTokenService()
        {
            _tokenHandler = new JwtSecurityTokenHandler();
            //TODO Add Secret handler for JWT key
            _key = Encoding.ASCII.GetBytes("We need to use a Secret Handler here");
        }

        public string GetToken(User user)
        {
            return GenerateToken(user);
        }

        private string GenerateToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Id), new Claim(ClaimTypes.Role, "Registered") }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = _tokenHandler.CreateToken(tokenDescriptor);

            return _tokenHandler.WriteToken(token);
        }

    }
}
