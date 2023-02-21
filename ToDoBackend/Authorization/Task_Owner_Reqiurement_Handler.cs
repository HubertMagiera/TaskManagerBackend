using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ToDoBackend.Entities.DTO_Models;

namespace ToDoBackend.Authorization
{
    public class Task_Owner_Reqiurement_Handler : AuthorizationHandler<Task_Owner_Reqiurement, Task_DTO>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, Task_Owner_Reqiurement requirement, Task_DTO task)
        {
            int userID = Convert.ToInt32(context.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (userID == task.user.user_id)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
