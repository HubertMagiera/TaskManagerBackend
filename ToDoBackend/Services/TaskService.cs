using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoBackend.Authorization;
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
        private readonly IAuthorizationService authorizationService;

        public TaskService(DatabaseContext _dbContext, IMapper _mapper,IHttpContextService _contextService, IAuthorizationService _authorizationService)
        {
            this.dbContext = _dbContext;
            this.mapper = _mapper;
            this.contextService = _contextService;
            this.authorizationService = _authorizationService;
        }
        public int AddNewTask(CreateTask taskToAdd)
        {
            var userID = contextService.GetUserID();//reads id of a user who created this request
            //map to dto before sending to db
            TaskDTO toAdd = mapper.Map<TaskDTO>(taskToAdd);

            UserDTO user = dbContext.user.FirstOrDefault(u => u.user_id == userID);
            if (user == null)
                throw new UserNotFoundException("Such user was not found.");

            TaskTypeDTO task_type = dbContext.task_type.FirstOrDefault(type => type.task_type_id == taskToAdd.task_type_id);//get task type for provided task type id
            if (task_type == null)
                throw new TaskTypeNotProvidedException("Please provide correct task type.");

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
            var taskToArchive = dbContext.task
                .Include(task => task.task_Type)
                .Include(task => task.user)
                .FirstOrDefault(task => task.task_id == id);//find task that should be archived
            if (taskToArchive == null)//if task not found, throw an error
                throw new TaskNotFoundException("There is no task for provided ID");

            //verification if user who wants to update a task is also its owner/creator
            var verificationResult = authorizationService.AuthorizeAsync(contextService.GetUser(), taskToArchive, new TaskOwnerReqiurement()).Result;
            if (!verificationResult.Succeeded)
                throw new UserNotAllowedException("You are not allowed to archive this task");

            taskToArchive.task_close_date = DateTime.Now;//set task close date
            taskToArchive.task_status = "Cancelled";//set task status
            dbContext.SaveChanges();
        }

        public List<ViewTask> GetAllTasksForUser()
        {
            var userID = contextService.GetUserID();//read user id
            var tasks = dbContext.task
                .Include(task => task.task_Type)
                .Include(task => task.user)
                .Where(task => task.user.user_id == userID)
                .ToList();//create list of tasks created by user
                
            return mapper.Map<List<ViewTask>>(tasks);
        }

        public ViewTask GetTaskByID(int id)
        {
            var task = dbContext.task
                .Include(task => task.task_Type)
                .Include(task => task.user)
                .FirstOrDefault(t => t.task_id == id);//find task by provided id
            if (task == null)//in case no task found, throw an error
                throw new TaskNotFoundException("There is no task for provided ID");

            //verification if user who wants to update a task is also its owner/creator
            var verificationResult = authorizationService.AuthorizeAsync(contextService.GetUser(), task, new TaskOwnerReqiurement()).Result;
            if (!verificationResult.Succeeded)
                throw new UserNotAllowedException("You are not allowed to view this task");

            return mapper.Map<ViewTask>(task);
        }

        public void UpdateTask(UpdateTask taskToUpdate)
        {
            int id = taskToUpdate.task_id;

            var task_type = dbContext.task_type.FirstOrDefault(type => type.task_type_id == taskToUpdate.task_type_id);//find task type for id provided
            if (task_type == null)//in case user did not provide id or id is wrong, throw an error
                throw new TaskTypeNotProvidedException("Task type not provided or not available in database");

            var taskFromDB = dbContext.task
                .Include(task => task.task_Type)
                .Include(task => task.user)
                .FirstOrDefault(task => task.task_id == id);//find task that needs to be updated
            if (taskFromDB == null)//in case task is not found, throw an error
                throw new TaskNotFoundException("There is no task for provided ID or this task was not created by current user");

            //verification if user who wants to update a task is also its owner/creator
            var verificationResult = authorizationService.AuthorizeAsync(contextService.GetUser(), taskFromDB, new TaskOwnerReqiurement()).Result;
            if (!verificationResult.Succeeded)
                throw new UserNotAllowedException("You are not allowed to update this task");

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
