using System.Security.Claims;
using ToDoBackend.Entities;
using ToDoBackend.Entities.DTO_Models;

namespace ToDoBackend.Services
{
    public interface ITokenService
    {
        string CreateToken(UserDTO user);
        string CreateRefreshToken();
        ClaimsPrincipal GetPrincipalFromOldToken(string oldToken);

    }
}
