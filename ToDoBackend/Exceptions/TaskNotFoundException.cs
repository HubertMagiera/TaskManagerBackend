﻿namespace ToDoBackend.Exceptions
{
    public class TaskNotFoundException:Exception
    {
        public TaskNotFoundException(string message) : base(message)
        {

        }
    }
}
