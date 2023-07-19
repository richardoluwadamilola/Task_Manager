using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Task_Manager.Data;
using Task_Manager.Models;
using TaskManager.Abstractions;
using TaskManager.EntityDTOs;

namespace TaskManager.Implementation
{
    public class TaskService : ITaskService
    {
        private readonly TaskManagerDBContext _context;
        public TaskService(TaskManagerDBContext context)
        {
            _context = context;
        }


        public TaskDto CreateTask(TaskDto taskDto)
        {
            var task = new Task_Manager.Models.Task
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                IsComplete = taskDto.IsComplete,
                Priority = taskDto.Priority,
                UserId = taskDto.UserId
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return taskDto;
        }

        public TaskDto GetTaskById(int taskId)
        {
            var task = _context.Tasks.Find(taskId);

            if (task != null)
            {
                return new TaskDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    IsComplete = task.IsComplete,
                    Priority = task.Priority,
                    UserId = task.UserId
                };
            }

#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public IEnumerable<TaskDto> GetAllTasks(int userId)
        {
            var tasks = _context.Tasks.Where(t => t.UserId == userId);

            foreach (var task in tasks)
            {
                yield return new TaskDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    IsComplete = task.IsComplete,
                    Priority = task.Priority,
                    UserId = task.UserId
                };
            }
        }

        public TaskDto UpdateTask(int taskId, TaskDto taskDto)
        {
            var task = _context.Tasks.Find(taskId);

            if (task != null)
            {
                task.Title = taskDto.Title;
                task.Description = taskDto.Description;
                task.IsComplete = taskDto.IsComplete;
                task.Priority = taskDto.Priority;
                task.UserId = taskDto.UserId;

                _context.SaveChanges();

                return taskDto;
            }

#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public TaskDto DeleteTask(int taskId)
        {
            var task = _context.Tasks.Find(taskId);

            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();

                return new TaskDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    IsComplete = task.IsComplete,
                    Priority = task.Priority,
                    UserId = task.UserId
                };
            }

#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public TaskDto MarkTaskComplete(int taskId)
        {
            var task = _context.Tasks.Find(taskId);

            if (task != null)
            {
                task.IsComplete = true;
                _context.SaveChanges();

                return new TaskDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    IsComplete = task.IsComplete,
                    Priority = task.Priority,
                    UserId = task.UserId
                };
            }

#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public TaskDto MarkTaskIncomplete(int taskId)
        {
            var task = _context.Tasks.Find(taskId);

            if (task != null)
            {
                task.IsComplete = false;
                _context.SaveChanges();

                return new TaskDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    IsComplete = task.IsComplete,
                    Priority = task.Priority,
                    UserId = task.UserId
                };
            }

#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public IEnumerable<TaskDto> GetTasksByPriority(int userId, PriorityLevel priority)
        {
            var tasks = _context.Tasks.Where(t => t.UserId == userId && t.Priority == priority);

            foreach (var task in tasks)
            {
                yield return new TaskDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    IsComplete = task.IsComplete,
                    Priority = task.Priority,
                    UserId = task.UserId
                };
            }
        }
    }

}
