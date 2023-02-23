using Microsoft.AspNetCore.Mvc;
using ToDoBackend.Entities;
using ToDoBackend.Entities.Create_Models;
using ToDoBackend.Services;

namespace ToDoBackend.Controllers
{
    [Route("taskManager/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<TokenModel> LoginUser([FromBody] LoginUser loginUser)
        {

            return Ok(userService.LoginUser(loginUser));
        }

        [HttpPost]
        [Route("refresh")]
        public ActionResult<TokenModel> RefreshAccessToken([FromBody]TokenModel model)
        {
            if (model == null)
                return BadRequest();

            return Ok(userService.RefreshToken(model));
        }

        [HttpPost]
        [Route("register")]
        public ActionResult CreateUser([FromBody] CreateUser createUser)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            userService.CreateUser(createUser);
            return new ObjectResult(null)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

    }
}
