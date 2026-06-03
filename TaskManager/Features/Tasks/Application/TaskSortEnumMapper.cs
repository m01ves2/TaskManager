using System.Linq.Expressions;
using TaskManager.Features.Tasks.Application.Models;
using TaskManager.Features.Tasks.Domain;

namespace TaskManager.Features.Tasks.Application
{
    public static class TaskSortEnumMapper
    {
        public static Expression<Func<TaskItem, object>> Map(TaskSortBy sortBy)
        {
            return sortBy switch
            {
                TaskSortBy.Id => x => x.Id,
                TaskSortBy.Title => x => x.Title,
                TaskSortBy.Status => x => x.Status,
                _ => x => x.Id
            };
        }
    }
}
