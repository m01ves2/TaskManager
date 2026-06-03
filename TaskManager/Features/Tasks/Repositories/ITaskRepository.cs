using TaskManager.Features.Tasks.Application.Models;
using TaskManager.Features.Tasks.Domain;

namespace TaskManager.Features.Tasks.Persistence
{
    public interface ITaskRepository
    {
        Task<PagedResult<TaskItem>> GetAllTasks(string? search, TaskItemStatus? status, int page, int pageSize, TaskSortBy sortBy, SortDirection sortDir);
        Task<TaskItem?> GetTaskById(int id);
        Task<TaskItem?> GetTaskByTitle(string title);
        Task<TaskItem> CreateTask(TaskItem item);
        Task<TaskItem?> UpdateTask(TaskItem item);
        Task<TaskItem?> DeleteTask(int id);
        Task SaveChanges();
    }
}
