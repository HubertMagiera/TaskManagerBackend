namespace ToDoBackend.Exceptions
{
    public class User_Already_Exists_Exception:Exception
    {
        public User_Already_Exists_Exception(string message) : base(message)
        {

        }
    }
}
