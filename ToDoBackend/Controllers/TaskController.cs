using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ToDoBackend.Entities.Create_Models;
using ToDoBackend.Entities.Update_Models;
using ToDoBackend.Entities.View_Models;
using ToDoBackend.Services;

namespace ToDoBackend.Controllers
{
    [Route("taskManager/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService taskService;
        public TaskController(ITaskService _taskService)
        {
            this.taskService = _taskService;
        }
        [HttpGet]
        public ActionResult<List<View_task>> GetTasksForUser()
        {
            //at the moment it returns all tasks from db,
            //no matter who created them
            return Ok(taskService.GetAllTasksForUser());
        }
        [HttpPost]
        public ActionResult AddNewTask([FromBody] Create_Task taskToAdd)
        {
            //check if all reqiured data is provided
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int taskID = taskService.AddNewTask(taskToAdd);

            return Created(String.Format("taskManager/tasks/{0}", taskID), null);

        }
        [HttpGet("{id}")]
        public ActionResult<View_task> GetTaskByID([FromRoute] int id)
        {
            return Ok(taskService.GetTaskByID(id));
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTask([FromRoute] int id)
        {
            //instead of deleting, it moves the task to cancelled stage
            taskService.DeleteTask(id);
            return NoContent();
        }

        [HttpPut]
        public ActionResult UpdateTask([FromBody] Update_Task taskToBeUpdated)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            taskService.UpdateTask(taskToBeUpdated);
            return Ok();
        }

    }
}
