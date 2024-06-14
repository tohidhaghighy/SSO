using Microsoft.IdentityModel.Tokens;
using SSO.Api.Infrastructure.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SSO.Api.Infrastructure.Common
{
    public static class GenerateJWTToken
    {
        // To generate token
        public static string GenerateToken(UserModel user, string key, string audience, string issuer)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.Username.ToString()),
                new Claim(ClaimTypes.Role,user.UserRole)
            };
            
            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
