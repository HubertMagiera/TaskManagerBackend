using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
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
        private readonly IPasswordHasher<UserDTO> hasher;
        private readonly ITokenService tokenService;

        public UserService(DatabaseContext _dbcontext, IMapper _mapper, IPasswordHasher<UserDTO> _hasher, ITokenService service)
        {
            mapper = _mapper;
            dbcontext = _dbcontext;
            hasher = _hasher;
            this.tokenService = service;
        }

        public void CreateUser(CreateUser create_user)
        {
            //first step is to validate if provided email address is valid
            bool emailCorrect = validateEmailFormat(create_user.user_email);
            if (emailCorrect == false)
                throw new NotEmailFormatException("Provided email address has invalid format");
            //then method verifies if there is already a user assigned to provided email address
            var userInDatabase = dbcontext.user.FirstOrDefault(u => u.user_email == create_user.user_email);
            if (userInDatabase != null)
                throw new UserAlreadyExistsException("Provided email address is already in use");
            //then method verifies if provided password meets reqiured rules
            bool passwordVerificationResult = validatePasswordMeetsRules(create_user.user_password);
            if (passwordVerificationResult == false)
                throw new PasswordDoesNotMeetRulesException("Provided password does not meet reqiured conditions. Please provide another one");

            //user is mapped into user_dto class which is the same as the database table
            var userToBeAdded = mapper.Map<UserDTO>(create_user);
            userToBeAdded.user_id = dbcontext.user.Max(u => u.user_id) +1;//read max user id from database and assign +1 as new user id
            userToBeAdded.user_password = hasher.HashPassword(userToBeAdded, create_user.user_password);//hash provided password

            dbcontext.user.Add(userToBeAdded);
            dbcontext.SaveChanges();
        }

        public TokenModel LoginUser(LoginUser login_user)
        {
            //this method logs in the user

            //verification if user for provided email address exists
            var user = dbcontext.user.FirstOrDefault(u => u.user_email == login_user.user_email);

            if (user == null)
                throw new UserNotFoundException("Provided credentials are wrong");
            //verification if password provided is the same as the one in database
            PasswordVerificationResult result = hasher.VerifyHashedPassword(user, user.user_password, login_user.user_password);
            if (result != PasswordVerificationResult.Success)
                throw new WrongCredentialsException("Provided credentials are wrong");

            string token = tokenService.CreateToken(user);//creating new access token
            string refreshToken = tokenService.CreateRefreshToken();//creating new refresh token
            user.user_refresh_token = refreshToken;//replace refresh tokens in database
            dbcontext.SaveChanges();

            return new TokenModel() 
                        { Access_Token = token, 
                          Refresh_Token = refreshToken
                        };

        }

        public TokenModel RefreshToken(TokenModel model)
        {
            //method used for refreshing request tokens

            ClaimsPrincipal principal = tokenService.GetPrincipalFromOldToken(model.Access_Token);
            int userID = Convert.ToInt32(principal.FindFirst(ClaimTypes.NameIdentifier).Value);

            var user = dbcontext.user.FirstOrDefault(u => u.user_id == userID);//find user based on info read from provided expired access token
            if (user == null)
                throw new UserNotFoundException("No user found for data assigned to this token");

            bool refreshTokenCorrect = user.user_refresh_token == model.Refresh_Token;//verify if refresh token in database is the same as the one provided
            //to be added: method to verify if refresh token is still valid
            if (!refreshTokenCorrect)
                throw new RefreshTokenInvalidException("Provided refresh token is invalid");

            string newRefreshToken = tokenService.CreateRefreshToken();//create new refresh token
            string newAccessToken = tokenService.CreateToken(user);//create new access token

            user.user_refresh_token = newRefreshToken;//replace refresh tokens in database
            dbcontext.SaveChanges();

            return new TokenModel()
                        {
                            Access_Token = newAccessToken,
                            Refresh_Token = newRefreshToken
                        };

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

            if (password.Length < 8 || !password.Any(char.IsUpper) || !password.Any(char.IsLower)|| !password.Any(char.IsDigit) || password.Any(char.IsWhiteSpace))
                return false;

            string specialCharacters = "!@#$%^&*+-/?<>;~`[]{}:,.|=_";//special characters. At least one of them needs to be present in provided password
            char[] specialCharactersArray = specialCharacters.ToCharArray();
            foreach(char ch in password)
            {
                if (specialCharactersArray.Contains(ch))//if char from password is visible in special characters list, return true
                    return true;
            }
            return false;
        }

        private bool validateEmailFormat(string emailAddress)
        {
            //this method validates if provided email address has correct format
            Regex pattern = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
            bool result = pattern.IsMatch(emailAddress);
            return result;
        }
    }
}
