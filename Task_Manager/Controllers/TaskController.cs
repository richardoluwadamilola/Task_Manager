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
                return BadRequest(ModelState);

            if (!Enum.IsDefined(typeof(PriorityLevel), taskDto.Priority))
                return BadRequest(new { Message = "Invalid priority value. Must be between 0 and 2." });

            var createdTask = _taskService.CreateTask(taskDto);
                return createdTask != null ? Ok(createdTask) : BadRequest("Failed to create task.");
        }

        [HttpGet ("{taskById}")]
        public IActionResult GetTaskById(int taskId)
        {
            var task = _taskService.GetTaskById(taskId);
            return task != null ? Ok(task) : BadRequest();
        }

        [HttpGet("{userId}/tasks")]
        public IActionResult GetAllTasks(int userId)
        {
            var tasks = _taskService.GetAllTasks(userId);
            return tasks != null ? Ok(tasks) : BadRequest();
        }

        [HttpPut("{userId}/tasks")]
        public IActionResult UpdateTask(int taskId, [FromBody] TaskDto taskDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return _taskService.UpdateTask(taskId, taskDto) != null ? Ok() : BadRequest();
        }

        [HttpDelete("{userId}/tasks")]
        public IActionResult DeleteTask(int taskId)
        {
            var deletedTask = _taskService.DeleteTask(taskId);
            return deletedTask != null ? Ok() : BadRequest();
        }

        [HttpPost("{userId}/tasks/complete")]
        public IActionResult MarkTaskComplete(int taskId)
        {
            var completedTask = _taskService.MarkTaskComplete(taskId);
            return completedTask != null ? Ok() : BadRequest();
        }

        [HttpPost("{userId}/tasks/incomplete")]
        public IActionResult MarkTaskIncomplete(int taskId)
        {
            var incompletedTask = _taskService.MarkTaskIncomplete(taskId);
            return incompletedTask != null ? Ok() : BadRequest();
        }

        [HttpGet("{userId}/tasks/priority")]
        public IActionResult GetTasksByPriority(int userId, PriorityLevel priority)
        {
            var tasksByPriority = _taskService.GetTasksByPriority(userId, priority);
            return tasksByPriority != null ? Ok() : BadRequest();
        }

    }
}
