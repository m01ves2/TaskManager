using System.ComponentModel.DataAnnotations;

namespace TaskManager.Dtos
{
    public class UpdateTaskItemDto
    {
        [Required]
        [MinLength(3)]
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
