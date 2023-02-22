namespace ToDoBackend
{
    // class which stores data about authentication from appsettings.json
    public class AuthenticationSettings
    {
        public string Key { get; set; }

        public string Audience { get; set; }

        public string Issuer { get; set; }

        public string KeyForRefreshToken { get; set; }
    }
}
