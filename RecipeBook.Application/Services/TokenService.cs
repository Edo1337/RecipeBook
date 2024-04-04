﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RecipeBook.Domain.Interfaces.Services;
using RecipeBook.Domain.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RecipeBook.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _jwtKey;
        private readonly string _issuer;
        private readonly string _audience;

        public TokenService(IOptions<JwtSettings> options)
        {
            _jwtKey = options.Value.JwtKey;
            _issuer = options.Value.Issuer;
            _audience = options.Value.Audience;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var credentialns = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(_issuer, _audience, claims, null, DateTime.UtcNow.AddMinutes(10), credentialns);

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }

        public string GenerateRefreshToken()
        {
            var randomNumbers = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumbers);
            return Convert.ToBase64String(randomNumbers);
        }
    }
}