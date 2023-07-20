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
                return BadRequest(ModelState);

            var createdUser = _userService.CreateUser(userDto);
            return createdUser != null ? Ok(createdUser) : BadRequest("Failed to create user.");
        }

        [HttpGet("{userById}")]
        public IActionResult GetUserById(int userId)
        {
            var user = _userService.GetUserById(userId);
            return user != null ? Ok(user) : BadRequest();
        }


        [HttpGet("user/{username}")]
        public IActionResult GetUserByUsername(string username)
        {
            var users = _userService.GetUserByUsername(username);
            return users != null ? Ok(users) : BadRequest();
        }

        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return users != null ? Ok(users) : BadRequest();
        }

        [HttpPut("user/{userId}")]
        public IActionResult UpdateUser(int userId, [FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedUser = _userService.UpdateUser(userId, userDto);
            return updatedUser != null ? Ok(updatedUser) : BadRequest("User not found or no updates were made.");
        }

        [HttpDelete("user/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var deletedUser = _userService.DeleteUser(userId);
            return deletedUser != null ? Ok(deletedUser) : BadRequest();
        }
    }
}
