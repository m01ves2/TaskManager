using System.ComponentModel.DataAnnotations;
using TaskManager.Services.Models;

namespace TaskManager.Dtos
{
    public class TaskQueryDto
    {
        public string? Search { get; set; }
        public bool? IsCompleted { get; set; }

        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        [Range(1, 100)]
        public int PageSize { get; set; } = 20;

        public TaskSortBy SortBy { get; set; } = TaskSortBy.Id;
        public SortDirection SortDirection { get; set; } = SortDirection.Asc;
    }
}
