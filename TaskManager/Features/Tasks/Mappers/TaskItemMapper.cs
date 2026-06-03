using TaskManager.Features.Tasks.Domain;
using TaskManager.Features.Tasks.Dtos;

namespace TaskManager.Features.Tasks.Mappers
{
    public static class TaskItemMapper
    {
        public static ReadTaskItemDto ToReadDto(TaskItem item)
        {
            return new ReadTaskItemDto()
            {
                Id = item.Id,
                Title = item.Title,
                Status = item.Status,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt,
            };
        }

        public static TaskItem FromCreateDto(CreateTaskItemDto createTaskItemDto)
        {
            return new TaskItem()
            {
                Title = createTaskItemDto.Title,
                //Status = TaskItemStatus.New,
            };
        }

        public static TaskItem FromUpdateDto(int id, UpdateTaskItemDto updateTaskItemDto)
        {
            return new TaskItem
            {
                Id = id,
                Title = updateTaskItemDto.Title,
                Status = updateTaskItemDto.Status,
            };
        }
    }
}
