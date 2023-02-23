namespace ToDoBackend.Exceptions
{
    public class UserNotFoundException :Exception
    {
        public UserNotFoundException(string message) : base(message)
        {

        }
    }
}
