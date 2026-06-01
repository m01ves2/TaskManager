using TaskManager.Models;
using TaskManager.Services.Models;

namespace TaskManager.Services
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetAllTasks(string? search, bool? isCompleted, int page, int pageSize, TaskSortBy sortBy, SortDirection sortDir);
        Task<TaskItem?> GetTaskById(int id);
        Task<TaskItem> CreateTask(TaskItem item);
        Task<TaskItem?> UpdateTask(TaskItem item);
        Task<TaskItem?> DeleteTask(int id);
        Task<TaskItem> CompleteTask(int id);
    }
}
