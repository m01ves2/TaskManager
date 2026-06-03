using Microsoft.AspNetCore.Mvc;
using TaskManager.Features.Tasks.Application;
using TaskManager.Features.Tasks.Application.Models;
using TaskManager.Features.Tasks.Domain;
using TaskManager.Features.Tasks.Dtos;
using TaskManager.Features.Tasks.Mappers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TaskManager.Features.Tasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        //private readonly ILogger<TaskController> _logger;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ReadTaskItemDto>>> GetAll([FromQuery] TaskQueryDto query)
        {
            var taskItemsPagedResult = await _taskService.GetAllTasks(query.Search, query.Status, query.Page, query.PageSize, query.SortBy, query.SortDirection);
            var taskItemsDtoPagedResult = new PagedResult<ReadTaskItemDto>
            {
                Items = taskItemsPagedResult.Items.Select(x => TaskItemMapper.ToReadDto(x)).ToList(),
                TotalCount = taskItemsPagedResult.TotalCount,
                Page = taskItemsPagedResult.Page,
                PageSize = taskItemsPagedResult.PageSize,
                TotalPages = taskItemsPagedResult.TotalPages
            };

            return Ok(taskItemsDtoPagedResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadTaskItemDto>> GetById(int id)
        {
            var taskItem = await _taskService.GetTaskById(id);
            var responseDto = TaskItemMapper.ToReadDto(taskItem);
            return Ok(responseDto);
        }

        [HttpGet("deleted")]
        public async Task<ActionResult<PagedResult<ReadTaskItemDto>>> GetDeleted([FromQuery] TaskQueryDto query)
        {
            var deletedTaskItemsPagedResult = await _taskService.GetDeletedTasks(query.Page, query.PageSize);
            var deletedTaskItemsDtoPagedResult = new PagedResult<ReadTaskItemDto>
            {
                Items = deletedTaskItemsPagedResult.Items.Select(x => TaskItemMapper.ToReadDto(x)).ToList(),
                TotalCount = deletedTaskItemsPagedResult.TotalCount,
                Page = deletedTaskItemsPagedResult.Page,
                PageSize = deletedTaskItemsPagedResult.PageSize,
                TotalPages = deletedTaskItemsPagedResult.TotalPages
            };

            return Ok(deletedTaskItemsDtoPagedResult);
        }

        [HttpPost]
        public async Task<ActionResult<ReadTaskItemDto>> Create(CreateTaskItemDto createDto)
        {
            var taskItem = TaskItemMapper.FromCreateDto(createDto);
            var createdTaskItem = await _taskService.CreateTask(taskItem);
            var responseDto = TaskItemMapper.ToReadDto(createdTaskItem);

            return CreatedAtAction(nameof(GetById),
                                    new { id = responseDto.Id },
                                    responseDto);
        }

        [HttpPost("{id}/complete")]
        public async Task<ActionResult<ReadTaskItemDto>> CompleteTask(int id)
        {
            var taskItem = await _taskService.CompleteTask(id);
            var responseDto = TaskItemMapper.ToReadDto(taskItem);
            return Ok(responseDto);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ReadTaskItemDto>> Update(int id, UpdateTaskItemDto updateDto)
        {
            var taskItem = TaskItemMapper.FromUpdateDto(id, updateDto);
            var updatedTaskItem = await _taskService.UpdateTask(taskItem);
            var responseDto = TaskItemMapper.ToReadDto(updatedTaskItem);
            return Ok(responseDto);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedTaskItem = await _taskService.DeleteTask(id);
            return NoContent();
        }

        [HttpPost("{id}/restore")]
        public async Task<ActionResult<ReadTaskItemDto>> Restore(int id)
        {
            var restoredTaskItem = await _taskService.RestoreTask(id);
            var responseDto = TaskItemMapper.ToReadDto(restoredTaskItem);
            return Ok(responseDto);
        }

        [HttpDelete("{id}/permanently")]
        public async Task<IActionResult> DeletePermanently(int id)
        {
            var deletedTaskItem = await _taskService.DeletePermanentlyTask(id);
            return NoContent();
        }
    }
}
