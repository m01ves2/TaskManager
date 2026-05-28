using Microsoft.AspNetCore.Mvc;
using TaskManager.Exceptions;
using TaskManager.Models;
using TaskManager.Repositories;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TaskItem>> GetAllTasks()
        {
            return await _repository.GetAllTasks();
        }

        public async Task<TaskItem?> GetTaskById(int id)
        {
            return await _repository.GetTaskById(id);
        }

        public async Task<TaskItem?> GetTaskByTitle(string title)
        {
            return await _repository.GetTaskByTitle(title);
        }

        public async Task<TaskItem> CreateTask(TaskItem item)
        {
            var existingTask = await _repository.GetTaskByTitle(item.Title);

            if(string.IsNullOrWhiteSpace(item.Title)) {
                throw new BusinessException("TASK_TITLE_INVALID", "Title cannot be empty");
            }

            if (existingTask != null) {
                throw new BusinessException("TASK_ALREADY_EXISTS", "Task already exists");
            }
            
            return await _repository.CreateTask(item);
        }

        public async Task<TaskItem?> UpdateTask(TaskItem item)
        {
            return await _repository.UpdateTask(item);
        }

        public async Task<TaskItem?> DeleteTask(int id)
        {
            return await _repository.DeleteTask(id);
        }
    }
}
