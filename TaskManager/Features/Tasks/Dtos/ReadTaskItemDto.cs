using TaskManager.Features.Tasks.Domain;

namespace TaskManager.Features.Tasks.Dtos
{
    public class ReadTaskItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        //public bool IsCompleted { get; set; }
        public TaskItemStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
