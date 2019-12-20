using DotNetSurfer.Core.TokenGenerators;
using DotNetSurfer_Backend.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNetSurfer_Backend.Infrastructure.TokenGenerators
{
    public class JwtGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtGenerator(IConfiguration configurations)
        {
            this._configuration = configurations;
        }

        public string GetToken(User user)
        {
            return BuildToken(user);
        }

        private string BuildToken(User user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserId.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Permission.PermissionType.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              issuer: this._configuration["Jwt:Issuer"],
              audience: this._configuration["Jwt:Issuer"],
              claims: claims,
              expires: DateTime.Now.AddMinutes(Convert.ToInt32(this._configuration["Jwt:ExpireMinutes"])),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
