using ToDoBackend.Entities.Create_Models;
using ToDoBackend.Entities.Update_Models;
using ToDoBackend.Entities.View_Models;

namespace ToDoBackend.Services
{
    public interface ITaskService
    {
        List<View_task> GetAllTasksForUser();

        View_task GetTaskByID(int id);

        void UpdateTask(Update_Task taskToUpdate);

        int AddNewTask(Create_Task taskToAdd);

        void DeleteTask(int id);
    }
}
