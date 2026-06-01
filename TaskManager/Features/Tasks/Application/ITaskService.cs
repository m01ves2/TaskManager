using TaskManager.Features.Tasks.Application.Models;
using TaskManager.Features.Tasks.Domain;

namespace TaskManager.Features.Tasks.Application
{
    public interface ITaskService
    {
        Task<PagedResult<TaskItem>> GetAllTasks(string? search, bool? isCompleted, int page, int pageSize, TaskSortBy sortBy, SortDirection sortDir);
        Task<TaskItem?> GetTaskById(int id);
        Task<TaskItem> CreateTask(TaskItem item);
        Task<TaskItem?> UpdateTask(TaskItem item);
        Task<TaskItem?> DeleteTask(int id);
        Task<TaskItem> CompleteTask(int id);
    }
}
