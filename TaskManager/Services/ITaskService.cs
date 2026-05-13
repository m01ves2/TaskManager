using TaskManager.Models;

namespace TaskManager.Services
{
    public interface ITaskService
    {
        List<TaskItem> GetAllTasks();
        TaskItem? GetTaskById(int id);
        TaskItem CreateTask(TaskItem item);
        TaskItem? UpdateTask(TaskItem item);
        TaskItem? DeleteTask(int id);

        Guid GetId();
    }
}
