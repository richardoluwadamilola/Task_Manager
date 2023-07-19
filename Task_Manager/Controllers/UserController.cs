using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_Manager.Data;
using TaskManager.Abstractions;
using TaskManager.EntityDTOs;
using TaskManager.Implementation;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TaskManagerDBContext _dbContext;
        private readonly IUserService _userService;

        public UserController(TaskManagerDBContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdUser = _userService.CreateUser(userDto);

            if (createdUser != null)
            {
                return Ok(createdUser);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create user.");
        }

        [HttpGet("{userById}")]
        public IActionResult GetUserById(int userId)
        {
            var user = _userService.GetUserById(userId);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();
        }


        [HttpGet("user/{username}")]
        public IActionResult GetUserByUsername(string username)
        {
            var users = _userService.GetUserByUsername(username);

            if (users != null)
            {
                return Ok(users);
            }

            return NotFound();
        }

        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();

            if (users != null)
            {
                return Ok(users);
            }

            return NotFound();
        }

        [HttpPut("user/{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedUser = _userService.UpdateUser(userId, userDto);

            if (updatedUser != null)
            {
                return Ok(updatedUser);
            }

            return NotFound();
        }

        [HttpDelete("user/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var deletedUser = _userService.DeleteUser(userId);

            if (deletedUser != null)
            {
                return Ok(deletedUser);
            }

            return NotFound();
        }
    }
}
