using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task_Manager.Data;
using Task_Manager.Models;
using TaskManager.Abstractions;
using TaskManager.EntityDTOs;


namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskManagerDBContext _dbContext;
        private readonly ITaskService _taskService;

        public TaskController(TaskManagerDBContext dbContext, ITaskService taskService)
        {
            _dbContext = dbContext;
            _taskService = taskService;
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskDto taskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate the priority value
            if (taskDto.Priority is < 0 or > (Task_Manager.Models.PriorityLevel)2)
            {
                ModelState.AddModelError("Priority", "Invalid priority value. Must be between 0 and 2.");
                return BadRequest(ModelState);
            }

            var createdTask = _taskService.CreateTask(taskDto);

            if (createdTask != null)
            {
                return Ok(createdTask);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create task.");

        }

        [HttpGet ("{taskById}")]
        public IActionResult GetTaskById(int taskId)
        {
            var task = _taskService.GetTaskById(taskId);

            if (task != null)
            {
                return Ok(task);
            }

            return NotFound();
        }

        [HttpGet("{userId}/tasks")]
        public IActionResult GetAllTasks(int userId)
        {
            var tasks = _taskService.GetAllTasks(userId);

            if (tasks != null)
            {
                return Ok(tasks);
            }

            return NotFound();
        }

        [HttpPut("{userId}/tasks")]
        public IActionResult UpdateTask(int taskId, [FromBody] TaskDto taskDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedTask = _taskService.UpdateTask(taskId, taskDto);

            if (updatedTask != null)
            {
                return Ok(updatedTask);
            }

            return NotFound();
        }

        [HttpDelete("{userId}/tasks")]
        public IActionResult DeleteTask(int taskId)
        {
            var deletedTask = _taskService.DeleteTask(taskId);

            if (deletedTask != null)
            {
                return Ok(deletedTask);
            }

            return NotFound();
        }

        [HttpPost("{userId}/tasks/complete")]
        public IActionResult MarkTaskComplete(int taskId)
        {
            var completedTask = _taskService.MarkTaskComplete(taskId);

            if (completedTask != null)
            {
                return Ok(completedTask);
            }

            return NotFound();
        }

        [HttpPost("{userId}/tasks/incomplete")]
        public IActionResult MarkTaskIncomplete(int taskId)
        {
            var incompletedTask = _taskService.MarkTaskIncomplete(taskId);

            if (incompletedTask != null)
            {
                return Ok(incompletedTask);
            }

            return NotFound();
        }

        [HttpGet("{userId}/tasks/priority")]
        public IActionResult GetTasksByPriority(int userId, PriorityLevel priority)
        {
            var tasksByPriority = _taskService.GetTasksByPriority(userId, priority);

            if (tasksByPriority != null)
            {
                return Ok(tasksByPriority);
            }

            return NotFound();
        }

    }
}
