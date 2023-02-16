using Microsoft.AspNetCore.Mvc;
using ToDoBackend.Entities;
using ToDoBackend.Services;

namespace ToDoBackend.Controllers
{
    [Route("taskManager/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ITokenService tokenService;
        private readonly IConfiguration config;

        public UserController(IUserService _userService, ITokenService _tokenService, IConfiguration _config)
        {
            userService = _userService;
            tokenService = _tokenService;
            config = _config;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult LoginUser([FromBody] LoginUser loginUser)
        {
            var user = userService.Get_User(loginUser);
            string token = tokenService.CreateToken(config["Jwt:Key"], config["Jwt:Issuer"], config["Jwt:Audience"], user);

            return Ok(token);
        }
    }
}
