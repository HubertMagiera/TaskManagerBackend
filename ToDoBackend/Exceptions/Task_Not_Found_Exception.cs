namespace ToDoBackend.Exceptions
{
    public class Task_Not_Found_Exception:Exception
    {
        public Task_Not_Found_Exception(string message) : base(message)
        {

        }
    }
}
