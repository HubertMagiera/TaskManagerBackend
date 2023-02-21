using System.Security.Claims;

namespace ToDoBackend.Services
{
    public class HttpContextService : IHttpContextService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public HttpContextService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public ClaimsPrincipal GetUser()
        {
            //will return claims for user. Works only if authorize section is in request, otherwise it is null
            return _contextAccessor.HttpContext.User;
        }

        public int GetUserID()
        {
            int userID = Convert.ToInt32(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            return userID;
        }
        
    }
}
