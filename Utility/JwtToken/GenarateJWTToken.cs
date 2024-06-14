using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SSO.Utility
{
    public static class GenerateJWTToken
    {
        // To generate token
        public static string GenerateToken(UserModel user, JwtInfo jwtInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtInfo.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("UserId",user.UserId.ToString()),
                new Claim("Username",user.Username.ToString()),
                new Claim("UserRole",user.UserRole.ToString())
            };
            
            var token = new JwtSecurityToken(
                jwtInfo.Issuer,
                jwtInfo.Audience,
                claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
