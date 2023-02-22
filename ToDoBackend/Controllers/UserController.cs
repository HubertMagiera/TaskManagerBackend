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
        public ActionResult<Token_model> LoginUser([FromBody] LoginUser loginUser)
        {

            return Ok(userService.Login_User(loginUser));
        }

        [HttpPost]
        [Route("refresh")]
        public ActionResult<Token_model> RefreshAccessToken([FromBody]Token_model model)
        {
            if (model == null)
                return BadRequest();

            return Ok(userService.Refresh_Token(model));
        }

        [HttpPost]
        [Route("register")]
        public ActionResult CreateUser([FromBody] Create_User createUser)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            userService.Create_User(createUser);
            return new ObjectResult(null)
            {
                StatusCode = StatusCodes.Status201Created
            };
        }

    }
}
