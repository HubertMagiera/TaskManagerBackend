using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoBackend.Entities;
using ToDoBackend.Entities.DTO_Models;

namespace ToDoBackend.Services
{
    public class TokenService : ITokenService
    {
        private readonly int Token_Valid_Time = 15;

        public string CreateToken(string key, string issuer,string audience, User_DTO user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.user_id.ToString()),
                new Claim(ClaimTypes.Name,user.user_name),
                new Claim(ClaimTypes.Surname,user.user_surname),
                new Claim(ClaimTypes.Email,user.user_email)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer:issuer, audience:audience, claims:claims,
                expires: DateTime.Now.AddMinutes(Token_Valid_Time), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
