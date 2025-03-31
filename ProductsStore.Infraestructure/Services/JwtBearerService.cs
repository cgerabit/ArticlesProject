using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using ProductsStore.Application.ConfigurationTemplates;
using ProductsStore.Application.Contracts;
using ProductsStore.Application.DTOs;
using ProductsStore.Domain.Entities;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductsStore.Infraestructure.Services
{
    public class JwtBearerService : IJwtProvider
    {
        private readonly JwtConfiguration _jwtConfiguration;
        public JwtBearerService(IOptions<JwtConfiguration> configuration)
        {
            _jwtConfiguration = configuration.Value;
        }

        public JwtResponse GetUserToken(User user, string? role)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(_jwtConfiguration.Key);
            DateTime expiration = DateTime.UtcNow.AddHours(_jwtConfiguration.ExpirationInHours);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(GetStandartClaims(user, role)),
                Issuer = _jwtConfiguration.Issuer,
                Audience = _jwtConfiguration.Audience,
                Expires = expiration,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);



            return new JwtResponse()
            {
                Expiration = expiration,
                Token = tokenHandler.WriteToken(token)
            };
        }

        private List<Claim> GetStandartClaims(User user, string role)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
            ];

            if (!string.IsNullOrEmpty(role))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
