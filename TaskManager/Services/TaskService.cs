using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
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

        private Guid _id = Guid.NewGuid();


        public TaskService()
        {
            Console.WriteLine("TaskService created");
        }
        public Guid GetId()
        {
            return _id;
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
