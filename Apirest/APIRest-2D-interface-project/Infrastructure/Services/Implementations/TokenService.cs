using APIRest_2D_interface_project.Domain.Entities;
using APIRest_2D_interface_project.Infrastructure.Configurations;
using APIRest_2D_interface_project.Infrastructure.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIRest_2D_interface_project.Infrastructure.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        public string GenerateJwtToken(User user)
        {
            var jwtConfiguration = _configuration.GetSection("JwtSettings").Get<JwtConfiguration>();
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwtConfiguration.Issuer,
                audience: jwtConfiguration.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(jwtConfiguration.ExpirationMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
