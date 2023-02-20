using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
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
            bool emailCorrect = validateEmailFormat(create_user.user_email);
            if (emailCorrect == false)
                throw new Not_Email_Format_Exception("Provided email address has invalid format");
            var userInDatabase = dbcontext.user.FirstOrDefault(u => u.user_email == create_user.user_email);
            if (userInDatabase != null)
                throw new User_Already_Exists_Exception("Provided email address is already in use");
            bool passwordVerificationResult = validatePasswordMeetsRules(create_user.user_password);
            if (passwordVerificationResult == false)
                throw new Password_Does_Not_Meet_Rules_Exception("Provided password does not meet reqiured conditions. Please provide another one");

            var userToBeAdded = mapper.Map<User_DTO>(create_user);
            userToBeAdded.user_id = dbcontext.user.Max(u => u.user_id) +1;
            userToBeAdded.user_password = hasher.HashPassword(userToBeAdded, create_user.user_password);

            dbcontext.user.Add(userToBeAdded);
            dbcontext.SaveChanges();
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

        private bool validatePasswordMeetsRules(string password)
        {
            //password needs to be:
            //minimum 8 characters long
            //contain at least one upper case letter
            //contain at least one lower case letter
            //contain at least on digit
            //can not contain whitespaces
            //must contain one of the special characters

            if (password.Length < 8 || !password.Any(char.IsUpper) || !password.Any(char.IsLower)|| !password.Any(char.IsDigit))
                return false;

            string specialCharacters = "!@#$%^&*+-/?<>;~`[]{}:,.|=_";
            char[] specialCharactersArray = specialCharacters.ToCharArray();
            foreach(char ch in password)
            {
                if (specialCharactersArray.Contains(ch))
                    return true;
            }
            return false;
        }

        private bool validateEmailFormat(string emailAddress)
        {
            Regex pattern = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
            bool result = pattern.IsMatch(emailAddress);
            return result;
        }
    }
}
