using System.Security.Claims;
using ToDoBackend.Entities;
using ToDoBackend.Entities.DTO_Models;

namespace ToDoBackend.Services
{
    public interface ITokenService
    {
        string CreateToken(User_DTO user);
        string CreateRefreshToken();
        ClaimsPrincipal GetPrincipalFromOldToken(string oldToken);

    }
}
