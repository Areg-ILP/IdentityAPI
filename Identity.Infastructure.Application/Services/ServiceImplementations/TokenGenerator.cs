using Identity.Infastructure.Application.Models.DetailsModels;
using Identity.Infastructure.Application.Services.ServiceAbstractions;
using Identity.Infastructure.Application.Utilities.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infastructure.Application.Services.ServiceImplementations
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly JWTConfigs _configs;

        public TokenGenerator(IOptions<JWTConfigs> configs)
        {
            _configs = configs.Value;
        }

        public string GenerateToken(UserDetailsModel user)
        {
            if (user == null)
            {
                return null;
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configs.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JWTCustomTokenTypes.Email, user.Email),
                new Claim(JWTCustomTokenTypes.Id, user.Id.ToString())
            };

            var now = DateTime.Now;
            var token = new JwtSecurityToken
            (
                issuer: _configs.ValidIssuer,
                audience: _configs.ValidAudience,
                notBefore: now,
                claims: claims,
                expires: now.AddMinutes(_configs.LifeTime),
                signingCredentials: credentials
            );

            var encoded = new JwtSecurityTokenHandler().WriteToken(token);
            return encoded;
        }
    }
}
