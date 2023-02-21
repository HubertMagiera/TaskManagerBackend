using System.Security.Claims;

namespace ToDoBackend.Services
{
    public interface IHttpContextService
    {
        int GetUserID();
        ClaimsPrincipal GetUser();
    }
}
