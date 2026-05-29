using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            //Console.WriteLine("TaskRepository created");
            _context = context;
        }

        public async Task<List<TaskItem>> GetAllTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskItem?> GetTaskById(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<TaskItem?> GetTaskByTitle(string title)
        {
            return await _context.Tasks.FirstOrDefaultAsync(i =>i.Title == title);
        }

        public async Task<TaskItem> CreateTask(TaskItem item)
        {
            _context.Tasks.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<TaskItem?> UpdateTask(TaskItem changedItem)
        {
            var item = await _context.Tasks.FirstOrDefaultAsync(i => i.Id == changedItem.Id);

            if (item == null)
                return null;

            item.Title = changedItem.Title;
            item.IsCompleted = changedItem.IsCompleted;

            await _context.SaveChangesAsync();

            return item;

        }

        public async Task<TaskItem?> DeleteTask(int id)
        {
            var item = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return null;

            _context.Tasks.Remove(item);
            await _context.SaveChangesAsync();
            
            return item;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
