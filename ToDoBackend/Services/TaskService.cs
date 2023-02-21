using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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
            var userID = contextService.GetUserID();//reads id of a user who created this request
            //map to dto before sending to db
            Task_DTO toAdd = mapper.Map<Task_DTO>(taskToAdd);

            User_DTO user = dbContext.user.FirstOrDefault(u => u.user_id == userID);
            if (user == null)
                throw new User_Not_Found_Exception("Such user was not found.");

            Task_type_DTO task_type = dbContext.task_type.FirstOrDefault(type => type.task_type_id == taskToAdd.task_type_id);//get task type for provided task type id
            if (task_type == null)
                throw new Task_Type_Not_Provided_Exception("Please provide correct task type.");

            toAdd.task_id = dbContext.task.Max(task => task.task_id) +1;
            toAdd.task_close_date = null;
            toAdd.task_creation_date = DateTime.Now;
            toAdd.task_Type = task_type;//assign task type
            toAdd.user = user;
            dbContext.task.Add(toAdd);
            dbContext.SaveChanges();
            return toAdd.task_id;

        }

        public void DeleteTask(int id)
        {
            var userID = contextService.GetUserID();//read user id
            var taskToArchive = dbContext.task
                .Include(task => task.task_Type)
                .Include(task => task.user)
                .FirstOrDefault(task => task.task_id == id && task.user.user_id == userID);//find task that should be archived
            if (taskToArchive == null)//if task not found, throw an error
                throw new Task_Not_Found_Exception("There is no task for provided ID or this task was not created by current user");
            
            taskToArchive.task_close_date = DateTime.Now;//set task close date
            taskToArchive.task_status = "Cancelled";//set task status
            dbContext.SaveChanges();
        }

        public List<View_task> GetAllTasksForUser()
        {
            var userID = contextService.GetUserID();//read user id
            var tasks = dbContext.task
                .Include(task => task.task_Type)
                .Include(task => task.user)
                .Where(task => task.user.user_id == userID)
                .ToList();//create list of tasks created by user
                
            return mapper.Map<List<View_task>>(tasks);
        }

        public View_task GetTaskByID(int id)
        {
            var userID = contextService.GetUserID();//read user id
            var task = dbContext.task
                .Include(task => task.task_Type)
                .Include(task => task.user)
                .FirstOrDefault(t => t.task_id == id && t.user.user_id == userID);//find task by provided id
            if (task == null)//in case no task found, throw an error
                throw new Task_Not_Found_Exception("There is no task for provided ID or this task was not created by current user");

            return mapper.Map<View_task>(task);
        }

        public void UpdateTask(Update_Task taskToUpdate)
        {
            var userID = contextService.GetUserID();//read user id
            int id = taskToUpdate.task_id;

            var task_type = dbContext.task_type.FirstOrDefault(type => type.task_type_id == taskToUpdate.task_type_id);//find task type for id provided
            if (task_type == null)//in case user did not provide id or id is wrong, throw an error
                throw new Task_Type_Not_Provided_Exception("Task type not provided or not available in database");

            var taskFromDB = dbContext.task
                .Include(task => task.task_Type)
                .Include(task => task.user)
                .FirstOrDefault(task => task.task_id == id && task.user.user_id == userID);//find task that needs to be updated
            if (taskFromDB == null)//in case task is not found, throw an error
                throw new Task_Not_Found_Exception("There is no task for provided ID or this task was not created by current user");
            //update values for the task
            taskFromDB.task_name = taskToUpdate.task_name;
            taskFromDB.task_description = taskToUpdate.task_description;
            if (taskToUpdate.task_status == "Completed" || taskToUpdate.task_status == "Cancelled")
                taskFromDB.task_close_date = DateTime.Now;//if user wants to close the task, assign closing date
            else
                taskFromDB.task_close_date = null;//otherwise, leave this value empty
            taskFromDB.task_priority = taskToUpdate.task_priority;
            taskFromDB.task_status = taskToUpdate.task_status;
            taskFromDB.task_Type = task_type;

            dbContext.SaveChanges();
        }
    }
}
