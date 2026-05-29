using System.ComponentModel.DataAnnotations;

namespace TaskManager.Dtos
{
    public class CreateTaskItemDto
    {
        [Required] //transport validation: API contract, malformed request, bad client input
        [MinLength(3)] //transport validation
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}