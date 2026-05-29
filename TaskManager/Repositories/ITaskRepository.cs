using TaskManager.Models;

namespace TaskManager.Repositories
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetAllTasks();
        Task<TaskItem?> GetTaskById(int id);
        Task<TaskItem?> GetTaskByTitle(string title);
        Task<TaskItem> CreateTask(TaskItem item);
        Task<TaskItem?> UpdateTask(TaskItem item);
        Task<TaskItem?> DeleteTask(int id);
        Task SaveChanges();
    }
}
