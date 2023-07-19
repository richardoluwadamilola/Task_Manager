using Task_Manager.Models;
using TaskManager.EntityDTOs;

namespace TaskManager.Abstractions
{
    public interface ITaskService
    {
       TaskDto CreateTask(TaskDto taskDto);
       TaskDto GetTaskById(int taskId);
       IEnumerable<TaskDto> GetAllTasks(int userId);
       TaskDto UpdateTask(int taskId, TaskDto taskDto);
       TaskDto DeleteTask(int taskId);
       TaskDto MarkTaskComplete(int taskId);
       TaskDto MarkTaskIncomplete(int taskId);
       IEnumerable<TaskDto> GetTasksByPriority(int userId, PriorityLevel priority);
       
    }
}
