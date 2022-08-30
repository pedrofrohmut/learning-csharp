using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Shop.Models;

namespace Shop.Services;

public static class TokenService
{
    public static string GenerateToken(User user, string jwtSecret) 
    {
        try {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtSecret);
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expirationDate = DateTime.Now.AddDays(30);
            var subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            });
            var descriptor = new SecurityTokenDescriptor() {
                Subject = subject,
                Expires = expirationDate,
                SigningCredentials = credentials
            };
            var securityToken = handler.CreateToken(descriptor);
            var token = handler.WriteToken(securityToken);
            return token;
        } catch (Exception e) {
            System.Console.WriteLine("Error to generate token. " + e.Message);
            System.Console.WriteLine(e.StackTrace);
            throw e;
        }
    }
}
