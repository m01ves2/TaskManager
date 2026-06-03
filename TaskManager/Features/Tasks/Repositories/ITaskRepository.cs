using TaskManager.Features.Tasks.Application.Models;
using TaskManager.Features.Tasks.Domain;

namespace TaskManager.Features.Tasks.Persistence
{
    public interface ITaskRepository
    {
        Task<PagedResult<TaskItem>> GetAllTasks(string? search, TaskItemStatus? status, int page, int pageSize, TaskSortBy sortBy, SortDirection sortDir);
        Task<TaskItem?> GetTaskById(int id);
        Task<PagedResult<TaskItem>> GetDeletedTasks(int page, int pageSize);
        Task<TaskItem?> GetTaskByTitle(string title);
        Task<TaskItem> CreateTask(TaskItem item);
        Task<TaskItem?> UpdateTask(TaskItem item);
        Task<TaskItem?> DeleteTask(int id);
        Task<TaskItem?> DeletePermanentlyTask(int id);
        Task<TaskItem?> RestoreTask(int id);
        Task SaveChanges();
    }
}
