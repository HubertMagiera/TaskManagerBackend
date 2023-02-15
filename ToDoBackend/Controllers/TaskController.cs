﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ToDoBackend.Entities.Create_Models;
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
            if (taskID != -1)
                return Created(String.Format("taskManager/tasks/{0}", taskID), null);
            else
                return BadRequest();
        }

    }
}