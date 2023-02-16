using System.Security.Claims;
using ToDoBackend.Entities;
using ToDoBackend.Entities.DTO_Models;

namespace ToDoBackend.Services
{
    public class TokenService : ITokenService
    {
        private readonly int Token_Valid_Time = 15;

        public string CreateToken(string key, string issuer, User_DTO user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.user_id.ToString()),
                new Claim(ClaimTypes.Name,user.user_name),
                new Claim(ClaimTypes.Surname,user.user_surname),
                new Claim(ClaimTypes.Email,user.user_email)
            };
        }
    }
}
