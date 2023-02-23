using Microsoft.AspNetCore.Authorization;

namespace ToDoBackend.Authorization
{
    public class TaskOwnerReqiurement:IAuthorizationRequirement
    {
    }
}
