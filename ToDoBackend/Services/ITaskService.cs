using ToDoBackend.Entities.Create_Models;
using ToDoBackend.Entities.Update_Models;
using ToDoBackend.Entities.View_Models;

namespace ToDoBackend.Services
{
    public interface ITaskService
    {
        List<ViewTask> GetAllTasksForUser();

        ViewTask GetTaskByID(int id);

        void UpdateTask(UpdateTask taskToUpdate);

        int AddNewTask(CreateTask taskToAdd);

        void DeleteTask(int id);
    }
}
