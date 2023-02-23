using ToDoBackend.Entities;
using ToDoBackend.Entities.Create_Models;
using ToDoBackend.Entities.DTO_Models;
using ToDoBackend.Entities.View_Models;

namespace ToDoBackend.Services
{
    public interface IUserService
    {
        void CreateUser(CreateUser create_user);

        TokenModel LoginUser(LoginUser login_user);

        TokenModel RefreshToken(TokenModel model);
    }
}
