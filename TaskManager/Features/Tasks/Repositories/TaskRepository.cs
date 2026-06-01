using Microsoft.EntityFrameworkCore;
using TaskManager.Features.Tasks.Application;
using TaskManager.Features.Tasks.Application.Models;
using TaskManager.Features.Tasks.Domain;
using TaskManager.Infrastructure;

namespace TaskManager.Features.Tasks.Persistence
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<TaskItem>> GetAllTasks(string? search, bool? isCompleted, int page, int pageSize, TaskSortBy sortBy, SortDirection sortDir)
        {
            var query = _context.Tasks.AsQueryable();
            if (!string.IsNullOrEmpty(search)) {
                var searchNormalized = search.Trim();
                query = query.Where(t => t.Title.Contains(searchNormalized));
            }
            if (isCompleted != null) {
                query = query.Where(t => t.IsCompleted == isCompleted);
            }

            var totalCount = await query.CountAsync();

            query = (sortDir == SortDirection.Desc) ?
                    query.OrderByDescending(TaskSortEnumMapper.Map(sortBy)) :
                    query.OrderBy(TaskSortEnumMapper.Map(sortBy));

            query = query.Skip((page - 1) * pageSize)
                         .Take(pageSize);

            var items = await query.ToListAsync();
            return new PagedResult<TaskItem>
            {
                Items = items,
                TotalCount = totalCount,
            };
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

        public async Task<TaskItem?> CompleteTask(int id)
        {
            var item = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return null;

            item.IsCompleted = true;

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
