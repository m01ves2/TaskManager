using TaskManager.Data;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Rspositories
{
    public class TaskRepository : ITaskRepository
    {
        //private static int _nextId = 1;
        //private static List<TaskItem> _items = new()
        //{
        //    new TaskItem
        //    {
        //        Id = _nextId++,
        //        Title = "Learn ASP.NET Core",
        //        IsCompleted = false
        //    },

        //    new TaskItem
        //    {
        //        Id = _nextId++,
        //        Title = "Learn Controllers",
        //        IsCompleted = true
        //    }
        //};

        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            Console.WriteLine("TaskRepository created");
            _context = context;
        }

        public List<TaskItem> GetAllTasks()
        {
            return _context.Tasks.ToList();
        }

        public TaskItem? GetTaskById(int id)
        {
            return _context.Tasks.FirstOrDefault(i => i.Id == id);
        }

        public TaskItem CreateTask(TaskItem item)
        {
            _context.Tasks.Add(item);
            _context.SaveChanges();
            return item;
        }
        public TaskItem? UpdateTask(TaskItem changedItem)
        {
            var item = _context.Tasks.FirstOrDefault(i => i.Id == changedItem.Id);

            if (item == null)
                return null;

            item.Title = changedItem.Title;
            item.IsCompleted = changedItem.IsCompleted;

            _context.SaveChanges();

            return item;

        }

        public TaskItem? DeleteTask(int id)
        {
            var item = _context.Tasks.FirstOrDefault(x => x.Id == id);

            if (item == null)
                return null;

            _context.Tasks.Remove(item);
            _context.SaveChanges();
            
            return item;
        }
    }
}
