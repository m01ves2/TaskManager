using Microsoft.AspNetCore.Mvc;
using TaskManager.Common.Errors;
using TaskManager.Features.Tasks.Application;
using TaskManager.Features.Tasks.Application.Models;
using TaskManager.Features.Tasks.Dtos;
using TaskManager.Features.Tasks.Mappers;

namespace TaskManager.Features.Tasks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        //private readonly ILogger<TaskController> _logger;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }


        /// <summary>
        /// Get All tasks with filtering, pagination and sorting 
        /// </summary>
        /// <param name="query">Parameters of filtering, pagination and sorting</param>
        /// <returns>Task list with meta data</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<ReadTaskItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
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


        /// <summary>
        /// Get task by id
        /// </summary>
        /// <param name="id">Task's id</param>
        /// <returns>Task</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReadTaskItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)] // NotFound через NotFoundException
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadTaskItemDto>> GetById(int id)
        {
            var taskItem = await _taskService.GetTaskById(id);
            var responseDto = TaskItemMapper.ToReadDto(taskItem);
            return Ok(responseDto);
        }


        /// <summary>
        /// Get All deleted tasks with pagination
        /// </summary>
        /// <param name="query">Parameters of pagination</param>
        /// <returns>Task list with meta data</returns>
        [HttpGet("deleted")]
        [ProducesResponseType(typeof(PagedResult<ReadTaskItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
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


        /// <summary>
        /// Create task
        /// </summary>
        /// <param name="createDto">Attributes for task creation</param>
        /// <returns>Created task</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ReadTaskItemDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)] // BusinessException
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadTaskItemDto>> Create(CreateTaskItemDto createDto)
        {
            var taskItem = TaskItemMapper.FromCreateDto(createDto);
            var createdTaskItem = await _taskService.CreateTask(taskItem);
            var responseDto = TaskItemMapper.ToReadDto(createdTaskItem);

            return CreatedAtAction(nameof(GetById),
                                    new { id = responseDto.Id },
                                    responseDto);
        }


        /// <summary>
        /// Update task
        /// </summary>
        /// <param name="id">Task's id</param>
        /// <param name="updateDto">Attributes to change</param>
        /// <returns>Updated task</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ReadTaskItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)] // NotFoundException
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)] // BusinessException
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadTaskItemDto>> Update(int id, UpdateTaskItemDto updateDto)
        {
            var taskItem = TaskItemMapper.FromUpdateDto(id, updateDto);
            var updatedTaskItem = await _taskService.UpdateTask(taskItem);
            var responseDto = TaskItemMapper.ToReadDto(updatedTaskItem);
            return Ok(responseDto);
        }


        /// <summary>
        /// Delete task
        /// </summary>
        /// <param name="id">Task's id</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)] // NotFoundException
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedTaskItem = await _taskService.DeleteTask(id);
            return NoContent();
        }

        /// <summary>
        /// Check task as completed
        /// </summary>
        /// <param name="id">Task's id</param>
        /// <returns>Completed task</returns>
        [HttpPost("{id}/complete")]
        [ProducesResponseType(typeof(ReadTaskItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadTaskItemDto>> CompleteTask(int id)
        {
            var taskItem = await _taskService.CompleteTask(id);
            var responseDto = TaskItemMapper.ToReadDto(taskItem);
            return Ok(responseDto);
        }

        /// <summary>
        /// Restore task
        /// </summary>
        /// <param name="id">Task's id to restore</param>
        /// <returns>Restored task</returns>
        [HttpPost("{id}/restore")]
        [ProducesResponseType(typeof(ReadTaskItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ReadTaskItemDto>> Restore(int id)
        {
            var restoredTaskItem = await _taskService.RestoreTask(id);
            var responseDto = TaskItemMapper.ToReadDto(restoredTaskItem);
            return Ok(responseDto);
        }

        /// <summary>
        /// Permanently delete task
        /// </summary>
        /// <param name="id">Task's id to delete permanently</param>
        [HttpDelete("{id}/permanently")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePermanently(int id)
        {
            var deletedTaskItem = await _taskService.DeletePermanentlyTask(id);
            return NoContent();
        }
    }
}
