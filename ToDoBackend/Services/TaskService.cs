using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToDoBackend.Entities.Create_Models;
using ToDoBackend.Entities.DTO_Models;
using ToDoBackend.Entities.Update_Models;
using ToDoBackend.Entities.View_Models;
using ToDoBackend.Exceptions;

namespace ToDoBackend.Services
{
    public class TaskService : ITaskService
    {
        private readonly IMapper mapper;
        private readonly DatabaseContext dbContext;
        private readonly IHttpContextService contextService;

        public TaskService(DatabaseContext _dbContext, IMapper _mapper,IHttpContextService _contextService)
        {
            this.dbContext = _dbContext;
            this.mapper = _mapper;
            this.contextService = _contextService;
        }
        public int AddNewTask(Create_Task taskToAdd)
        {
            var userID = contextService.GetUserID();
            //map to dto before sending to db
            Task_DTO toAdd = mapper.Map<Task_DTO>(taskToAdd);
            User_DTO user = dbContext.user.FirstOrDefault(u => u.user_id == userID);
            if (user == null)
                throw new User_Not_Found_Exception("Such user was not found.");
            toAdd.user = user;
            
            Task_type_DTO task_type = dbContext.task_type.FirstOrDefault(type => type.task_type_id == taskToAdd.task_type_id);
            if (task_type == null)
                throw new Task_Type_Not_Provided_Exception("Please provide correct task type.");

            toAdd.task_Type = task_type;
            dbContext.task.Add(toAdd);
            dbContext.SaveChanges();
            return toAdd.task_id;

        }

        public void DeleteTask(int id)
        {
            var userID = contextService.GetUserID();
            var taskToArchive = dbContext.task
                .Include(task => task.task_Type)
                .Include(task => task.user)
                .FirstOrDefault(task => task.task_id == id && task.user.user_id == userID);
            if (taskToArchive == null)
                throw new Task_Not_Found_Exception("There is no task for provided ID or this task was not created by current user");
            
            taskToArchive.task_close_date = DateTime.Now;
            taskToArchive.task_status = "Cancelled";
            dbContext.SaveChanges();
        }

        public List<View_task> GetAllTasksForUser()
        {
            var userID = contextService.GetUserID();
            var tasks = dbContext.task
                .Include(task => task.task_Type)
                .Include(task => task.user)
                .Where(task => task.user.user_id == userID)
                .ToList();
                
            return mapper.Map<List<View_task>>(tasks);
        }

        public View_task GetTaskByID(int id)
        {
            var userID = contextService.GetUserID();
            var task = dbContext.task
                .Include(task => task.task_Type)
                .Include(task => task.user)
                .FirstOrDefault(t => t.task_id == id && t.user.user_id == userID);
            if (task == null)
                throw new Task_Not_Found_Exception("There is no task for provided ID or this task was not created by current user");

            return mapper.Map<View_task>(task);
        }

        public void UpdateTask(Update_Task taskToUpdate)
        {
            var userID = contextService.GetUserID();
            int id = taskToUpdate.task_id;

            var task_type = dbContext.task_type.FirstOrDefault(type => type.task_type_id == taskToUpdate.task_type_id);
            if (task_type == null)
                throw new Task_Type_Not_Provided_Exception("Task type not provided or not available in database");

            var taskFromDB = dbContext.task
                .Include(task => task.task_Type)
                .Include(task => task.user)
                .FirstOrDefault(task => task.task_id == id && task.user.user_id == userID);
            if (taskFromDB == null)
                throw new Task_Not_Found_Exception("There is no task for provided ID or this task was not created by current user");
            taskFromDB.task_name = taskToUpdate.task_name;
            taskFromDB.task_description = taskToUpdate.task_description;
            if (taskToUpdate.task_status == "Completed" || taskToUpdate.task_status == "Cancelled")
                taskFromDB.task_close_date = DateTime.Now;
            else
                taskFromDB.task_close_date = null;
            taskFromDB.task_priority = taskToUpdate.task_priority;
            taskFromDB.task_status = taskToUpdate.task_status;
            taskFromDB.task_Type = task_type;

            dbContext.SaveChanges();
        }
    }
}
