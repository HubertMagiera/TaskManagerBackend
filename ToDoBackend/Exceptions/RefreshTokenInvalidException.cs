namespace ToDoBackend.Exceptions
{
    public class RefreshTokenInvalidException:Exception
    {
        public RefreshTokenInvalidException(string message) : base(message)
        {

        }
    }
}
