using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ToDoBackend.Entities;
using ToDoBackend.Entities.Create_Models;
using ToDoBackend.Entities.DTO_Models;
using ToDoBackend.Entities.View_Models;
using ToDoBackend.Exceptions;

namespace ToDoBackend.Services
{
    public class UserService : IUserService
    {

        private readonly IMapper mapper;
        private readonly DatabaseContext dbcontext;
        private readonly IPasswordHasher<User_DTO> hasher;

        public UserService(DatabaseContext _dbcontext, IMapper _mapper, IPasswordHasher<User_DTO> _hasher)
        {
            mapper = _mapper;
            dbcontext = _dbcontext;
            hasher = _hasher;
        }

        public void Create_User(Create_User create_user)
        {
            throw new NotImplementedException();
        }

        public User_DTO Get_User(LoginUser login_user)
        {
            var user = dbcontext.user.FirstOrDefault(u => u.user_email == login_user.user_email);
            if (user == null)
                throw new User_Not_Found_Exception("Provided credentials are wrong");
            PasswordVerificationResult result = hasher.VerifyHashedPassword(user, user.user_password, login_user.user_password);
            if (result != PasswordVerificationResult.Success)
                throw new Wrong_Credentials_Exception("Provided credentials are wrong");
            return user;

        }
    }
}
