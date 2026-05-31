using System.ComponentModel.DataAnnotations;

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
    }
}
