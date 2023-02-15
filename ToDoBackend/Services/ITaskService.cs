using ToDoBackend.Entities.Create_Models;
using ToDoBackend.Entities.View_Models;

namespace ToDoBackend.Services
{
    public interface ITaskService
    {
        List<View_task> GetAllTasksForUser();

        View_task GetTaskByID(int id);

        bool UpdateTask(View_task taskToUpdate, int taskID);

        int AddNewTask(Create_Task taskToAdd);

        bool DeleteTask(View_task taskToRemove);
    }
}
