using ToDoBackend.Entities;
using ToDoBackend.Entities.Create_Models;
using ToDoBackend.Entities.View_Models;

namespace ToDoBackend.Services
{
    public interface IUserService
    {
        void CreateUser(Create_User user);

        string LoginUser(LoginUser user);
    }
}
