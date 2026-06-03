using TaskManager.Common.Errors;
using TaskManager.Common.Exceptions;
using TaskManager.Features.Tasks.Application;
using TaskManager.Features.Tasks.Application.Models;
using TaskManager.Features.Tasks.Domain;
using TaskManager.Features.Tasks.Persistence;

namespace TaskManager.Common.Features.Tasks.Application
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<TaskItem>> GetAllTasks(string? search, TaskItemStatus? status, int page, int pageSize, TaskSortBy sortBy, SortDirection sortDir)
        {
            var result = await _repository.GetAllTasks(search, status, page, pageSize, sortBy, sortDir);
            result.Page = page;
            result.PageSize = pageSize;
            result.TotalPages = (int)Math.Ceiling(result.TotalCount / (double)pageSize);
            return result;
        }

        public async Task<TaskItem> GetTaskById(int id)
        {
            var result = await _repository.GetTaskById(id);

            if (result == null)
                throw new NotFoundException(ErrorCodes.TaskDoesNotExist, "Task doesn't exist");

            return result;
        }

        public async Task<TaskItem> CreateTask(TaskItem item)
        {
            item.Title = item.Title.Trim();

            ValidateTitle(item);

            var existing = await _repository.GetTaskByTitle(item.Title);

            if (existing != null)
                throw new BusinessException(ErrorCodes.TaskAlreadyExists, "Task already exists");

            item.Status = TaskItemStatus.New;
            item.CreatedAt = DateTime.UtcNow;
            item.UpdatedAt = DateTime.UtcNow;

            return await _repository.CreateTask(item);
        }

        public async Task<TaskItem> UpdateTask(TaskItem item)
        {
            item.Title = item.Title.Trim();

            ValidateTitle(item);

            var existing = await _repository.GetTaskByTitle(item.Title);

            if (existing != null && existing.Id != item.Id)
                throw new BusinessException(ErrorCodes.TaskAlreadyExists, "Task already exists");

            item.UpdatedAt = DateTime.UtcNow;
            var result = await _repository.UpdateTask(item);
            if (result == null)
                throw new NotFoundException(ErrorCodes.TaskDoesNotExist, "Task doesn't exist");

            return result;
        }

        private void ValidateTitle(TaskItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Title)) //business defensive validation
                throw new BusinessException(ErrorCodes.TaskTitleInvalid, "Title cannot be empty");
        }

        public async Task<TaskItem> DeleteTask(int id)
        {
            var result = await _repository.DeleteTask(id);
            if (result == null)
                throw new NotFoundException(ErrorCodes.TaskDoesNotExist, "Task doesn't exist");

            return result;
        }

        public async Task<TaskItem> CompleteTask(int id)
        {
            var item = await _repository.GetTaskById(id);

            if (item == null)
                throw new NotFoundException(ErrorCodes.TaskDoesNotExist, "Task doesn't exist");

            item.Status = TaskItemStatus.Completed;
            item.UpdatedAt = DateTime.UtcNow;

            var result = await _repository.UpdateTask(item);
            if (result == null)
                throw new NotFoundException(ErrorCodes.TaskDoesNotExist, "Task doesn't exist");

            return result;
        }
    }
}
