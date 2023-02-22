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
        private readonly int Token_Valid_Time = 15;//generated token is valid for 15 minutes
        private readonly int Refresh_Token_Valid_Time = 10080;//refresh token is valid for one week
        private readonly AuthenticationSettings settings;

        public TokenService(AuthenticationSettings _settings)
        {
            this.settings = _settings;
        }

        public string CreateToken(User_DTO user)
        {
            var claims = new List<Claim>()//claims represent info about the user
            {
                new Claim(ClaimTypes.NameIdentifier,user.user_id.ToString()),
                new Claim(ClaimTypes.Name,user.user_name),
                new Claim(ClaimTypes.Surname,user.user_surname),
                new Claim(ClaimTypes.Email,user.user_email)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer:settings.Issuer, audience:settings.Audience, claims:claims,
                expires: DateTime.Now.AddMinutes(Token_Valid_Time), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public string CreateRefreshToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.KeyForRefreshToken));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer: settings.Issuer, audience: settings.Audience,
                expires: DateTime.Now.AddMinutes(Refresh_Token_Valid_Time), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public ClaimsPrincipal GetPrincipalFromOldToken(string oldToken)
        {
            var validationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = settings.Issuer,
                ValidAudience = settings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(oldToken, validationParameters, out SecurityToken validatedToken);

            return principal;


        }
    }
}
