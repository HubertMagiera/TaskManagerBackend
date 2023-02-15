using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoBackend.Entities.Create_Models;
using ToDoBackend.Entities.DTO_Models;
using ToDoBackend.Entities.View_Models;
using ToDoBackend.Exceptions;

namespace ToDoBackend.Services
{
    public class TaskService : ITaskService
    {
        private readonly IMapper mapper;
        private readonly DatabaseContext dbContext;

        public TaskService(DatabaseContext _dbContext, IMapper _mapper)
        {
            this.dbContext = _dbContext;
            this.mapper = _mapper;
        }
        public int AddNewTask(Create_Task taskToAdd)
        {

            //map to dto before sending to db
            Task_DTO toAdd = mapper.Map<Task_DTO>(taskToAdd);
            User_DTO user = dbContext.user.FirstOrDefault(u => u.user_id == 1);//to be replaced later
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
            var taskToArchive = dbContext.task
                .Include(task => task.task_Type)
                .Include(task => task.user)
                .FirstOrDefault(task => task.task_id == id && task.user.user_id == 1);//to be redeveloped to compare with real user id
            if (taskToArchive == null)
                throw new Task_Not_Found_Exception("There is no task for provided ID or this task was not created by current user");
            
            taskToArchive.task_close_date = DateTime.Now;
            taskToArchive.task_status = "Cancelled";
            dbContext.SaveChanges();
        }

        public List<View_task> GetAllTasksForUser()
        {
            //at the moment it will be getting all tasks from db
            //to be redeveloped once jwt token is implemented
            var tasks = dbContext.task
                .Include(task => task.task_Type)
                .Include(task => task.user)
                .ToList();
                
            return mapper.Map<List<View_task>>(tasks);
        }

        public View_task GetTaskByID(int id)
        {
            //at the moment it will return specified task regardless who asks for it
            //to be redeveloped to check if task owner asks for the task
            //if user who asks == creator of a task and task id == provided id
            var task = dbContext.task
                .Include(task => task.task_Type)
                .Include(task => task.user)
                .FirstOrDefault(t => t.task_id == id && t.user.user_id == 1);//to be redeveloped to compare with real user id
            if (task == null)
                throw new Task_Not_Found_Exception("There is no task for provided ID or this task was not created by current user");

            return mapper.Map<View_task>(task);
        }

        public bool UpdateTask(View_task taskToUpdate, int taskID)
        {
            throw new NotImplementedException();
        }
    }
}
