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

        public bool DeleteTask(View_task taskToRemove)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public bool UpdateTask(View_task taskToUpdate, int taskID)
        {
            throw new NotImplementedException();
        }
    }
}
