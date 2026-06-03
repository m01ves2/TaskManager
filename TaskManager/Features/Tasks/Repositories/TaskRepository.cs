using System.Threading.Tasks;
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

        public async Task<PagedResult<TaskItem>> GetAllTasks(string? search, TaskItemStatus? status, int page, int pageSize, TaskSortBy sortBy, SortDirection sortDir)
        {
            var query = _context.Tasks.AsQueryable();
            query = query.Where(x => x.DeletedAt == null);

            if (!string.IsNullOrEmpty(search)) {
                var searchNormalized = search.Trim();
                query = query.Where(t => t.Title.Contains(searchNormalized));
            }
            if (status != null) {
                query = query.Where(t => t.Status == status);
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

        public async Task<PagedResult<TaskItem>> GetDeletedTasks(int page, int pageSize)
        {
            var query = _context.Tasks.AsQueryable();
            query = query.Where(x => x.DeletedAt != null);
            query = query.OrderByDescending(x => x.DeletedAt);

            var totalCount = await query.CountAsync();

            query = query.Skip((page - 1) * pageSize)
                         .Take(pageSize);
            var items = await query.ToListAsync();

            return new PagedResult<TaskItem>
            {
                Items = items,
                TotalCount = totalCount,
            };
        }

        public async Task<TaskItem?> GetTaskByTitle(string title)
        {
            return await _context.Tasks.FirstOrDefaultAsync(i => i.Title == title);
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
            item.Status = changedItem.Status;
            item.UpdatedAt = changedItem.UpdatedAt;
            item.CreatedAt = changedItem.CreatedAt;

            await _context.SaveChangesAsync();

            return item;

        }

        public async Task<TaskItem?> DeleteTask(int id)
        {
            var item = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return null;

            //_context.Tasks.Remove(item);
            item.DeletedAt = DateTime.UtcNow;
            item.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<TaskItem?> RestoreTask(int id)
        {
            var item = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return null;

            //_context.Tasks.Remove(item);
            item.DeletedAt = null;
            item.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<TaskItem?> DeletePermanentlyTask(int id)
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
