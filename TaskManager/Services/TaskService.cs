using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Rspositories;

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

        public async Task<TaskItem> CreateTask(TaskItem item)
        {
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
