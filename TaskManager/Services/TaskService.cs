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

            ValidateCreateTask(item);
            await EnsureTitleIsUnique(item);

            return await _repository.CreateTask(item);
        }

        private void ValidateCreateTask(TaskItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Title))
                throw new BusinessException("TASK_TITLE_INVALID", "Title cannot be empty");
        }
        private async Task EnsureTitleIsUnique(TaskItem item)
        {
            var existing = await _repository.GetTaskByTitle(item.Title);

            if (existing != null)
                throw new BusinessException("TASK_ALREADY_EXISTS", "Task already exists");
        }

        public async Task<TaskItem?> UpdateTask(TaskItem item)
        {
            return await _repository.UpdateTask(item);
        }

        public async Task<TaskItem?> DeleteTask(int id)
        {
            return await _repository.DeleteTask(id);
        }

        public async Task<TaskItem?> CompleteTask(int id)
        {
            var taskItem = await _repository.GetTaskById(id);
            if (taskItem == null)
                throw new BusinessException("TASK_DOESNOT_EXIST", "Task doesn't exist");

            taskItem.MarkAsCompleted();
            await _repository.SaveChanges();
            
            return taskItem;
        }
    }
}
