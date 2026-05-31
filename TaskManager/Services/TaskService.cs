using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Errors;
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

        public async Task<List<TaskItem>> GetAllTasks(string? search, bool? isCompleted, int page, int pageSize)
        {
            return await _repository.GetAllTasks(search, isCompleted, page, pageSize);
        }

        public async Task<TaskItem?> GetTaskById(int id)
        {
            return await _repository.GetTaskById(id);
        }

        public async Task<TaskItem> CreateTask(TaskItem item)
        {
            item.Title = item.Title.Trim();

            ValidateTitle(item);

            var existing = await _repository.GetTaskByTitle(item.Title);

            if (existing != null)
                throw new BusinessException(ErrorCodes.TaskAlreadyExists, "Task already exists");

            return await _repository.CreateTask(item);
        }

        public async Task<TaskItem?> UpdateTask(TaskItem item)
        {
            item.Title = item.Title.Trim();

            ValidateTitle(item);

            var existing = await _repository.GetTaskByTitle(item.Title);

            if (existing != null && existing.Id != item.Id)
                throw new BusinessException(ErrorCodes.TaskAlreadyExists, "Task already exists");

            return await _repository.UpdateTask(item);
        }

        private void ValidateTitle(TaskItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Title)) //business defensive validation
                throw new BusinessException(ErrorCodes.TaskTitleInvalid, "Title cannot be empty");
        }

        public async Task<TaskItem?> DeleteTask(int id)
        {
            return await _repository.DeleteTask(id);
        }

        public async Task<TaskItem> CompleteTask(int id)
        {
            var taskItem = await _repository.CompleteTask(id);

            if (taskItem == null)
                throw new BusinessException(ErrorCodes.TaskDoesNotExist, "Task doesn't exist");

            return taskItem;
        }
    }
}
