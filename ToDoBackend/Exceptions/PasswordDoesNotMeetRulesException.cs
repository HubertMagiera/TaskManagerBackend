﻿namespace ToDoBackend.Exceptions
{
    public class PasswordDoesNotMeetRulesException:Exception
    {
        public PasswordDoesNotMeetRulesException(string message):base(message)
        {
            
        }
    }
}
