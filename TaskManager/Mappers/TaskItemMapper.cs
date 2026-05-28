using TaskManager.Dtos;
using TaskManager.Models;

namespace TaskManager.Mappers
{
    public static class TaskItemMapper
    {
        public static ReadTaskItemDto ToReadDto(TaskItem item)
        {
            return new ReadTaskItemDto()
            {
                Id = item.Id,
                Title = item.Title,
                IsCompleted = item.IsCompleted
            };
        }

        public static TaskItem FromCreateDto(CreateTaskItemDto createTaskItemDto)
        {
            return new TaskItem()
            {
                Title = createTaskItemDto.Title,
                IsCompleted = createTaskItemDto.IsCompleted
            };
        }

        public static TaskItem FromUpdateDto(int id, UpdateTaskItemDto updateTaskItemDto)
        {
            return new TaskItem
            {
                Id = id,
                Title = updateTaskItemDto.Title,
                IsCompleted = updateTaskItemDto.IsCompleted
            };
        }
    }
}
