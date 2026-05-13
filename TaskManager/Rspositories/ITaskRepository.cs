using TaskManager.Models;

namespace TaskManager.Rspositories
{
    public interface ITaskRepository
    {
        List<TaskItem> GetAllTasks();
        TaskItem? GetTaskById(int id);
        TaskItem CreateTask(TaskItem item);
        TaskItem? UpdateTask(TaskItem item);
        TaskItem? DeleteTask(int id);
    }
}
