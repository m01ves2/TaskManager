using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Rspositories
{
    public class TaskRepository : ITaskRepository
    {
        private ILoggerService _loggerService;

        private static int _nextId = 1;
        private static List<TaskItem> _items = new()
        {
            new TaskItem
            {
                Id = _nextId++,
                Title = "Learn ASP.NET Core",
                IsCompleted = false
            },

            new TaskItem
            {
                Id = _nextId++,
                Title = "Learn Controllers",
                IsCompleted = true
            }
        };

        public TaskRepository(ILoggerService loggerService)
        {
            Console.WriteLine("TaskRepository created");
            _loggerService = loggerService;
        }

        public List<TaskItem> GetAllTasks() => _items;
        public TaskItem? GetTaskById(int id) => _items.FirstOrDefault(i => i.Id == id);

        public TaskItem CreateTask(TaskItem item)
        {
            item.Id = _nextId++;
            _items.Add(item);

            return item;
        }
        public TaskItem? UpdateTask(TaskItem changedItem)
        {
            var item = _items.FirstOrDefault(x => x.Id == changedItem.Id);

            if (item != null) {
                item.Title = changedItem.Title;
                item.IsCompleted = changedItem.IsCompleted;
            }

            return item;
        }

        public TaskItem? DeleteTask(int id)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);

            if (item != null) {
                _items.Remove(item);
            }

            return item;
        }
    }
}
