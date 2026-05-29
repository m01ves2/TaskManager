using TaskManager.Models;

namespace TaskManager.Services
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetAllTasks();
        Task<TaskItem?> GetTaskById(int id);
        Task<TaskItem?> GetTaskByTitle(string title);
        Task<TaskItem> CreateTask(TaskItem item);
        Task<TaskItem?> UpdateTask(TaskItem item);
        Task<TaskItem?> DeleteTask(int id);
        Task<TaskItem?> CompleteTask(int id);
    }
}
