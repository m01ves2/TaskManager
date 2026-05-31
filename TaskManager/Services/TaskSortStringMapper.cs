using System.Linq.Expressions;
using TaskManager.Models;
using TaskManager.Services.Models;

namespace TaskManager.Services
{
    public static class TaskSortStringMapper
    {
        public static TaskSortBy MapSortBy(string sortBy)
        {
            return sortBy switch
            {
                "Title" => TaskSortBy.Title,
                "IsCompleted" => TaskSortBy.IsCompleted,
                "Id" => TaskSortBy.Id,
                _  => TaskSortBy.Id
            };
        }

        public static SortDirection MapSortDir(string sortDir)
        {
            return sortDir switch
            {
                "asc" => SortDirection.Asc,
                "desc" => SortDirection.Desc,
                _ => SortDirection.Asc
            };
        }
    }
}
