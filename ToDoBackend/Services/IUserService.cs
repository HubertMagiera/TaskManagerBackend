using ToDoBackend.Entities;
using ToDoBackend.Entities.Create_Models;
using ToDoBackend.Entities.DTO_Models;
using ToDoBackend.Entities.View_Models;

namespace ToDoBackend.Services
{
    public interface IUserService
    {
        void Create_User(Create_User create_user);

        Token_model Login_User(LoginUser login_user);

        Token_model Refresh_Token(Token_model model);
    }
}
