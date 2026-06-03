namespace TaskManager.Features.Tasks.Domain
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        //public bool IsCompleted { get; set; }
        public TaskItemStatus Status {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
