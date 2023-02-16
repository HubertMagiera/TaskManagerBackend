using ToDoBackend.Entities;
using ToDoBackend.Entities.DTO_Models;

namespace ToDoBackend.Services
{
    public interface ITokenService
    {
        string CreateToken(string key, string issuer,string audience, User_DTO user);
    }
}
