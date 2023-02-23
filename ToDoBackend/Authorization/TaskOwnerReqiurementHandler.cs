using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ToDoBackend.Entities.DTO_Models;

namespace ToDoBackend.Authorization
{
    public class TaskOwnerReqiurementHandler : AuthorizationHandler<TaskOwnerReqiurement, TaskDTO>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TaskOwnerReqiurement requirement, TaskDTO task)
        {
            int userID = Convert.ToInt32(context.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (userID == task.user.user_id)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
