using Microsoft.AspNetCore.Mvc;
using TaskManager.Dtos;
using TaskManager.Mappers;
using TaskManager.Services;

namespace TaskManager.Controllers
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
        public async Task<ActionResult<List<ReadTaskItemDto>>> GetAll()
        {
            var taskItems = await _taskService.GetAllTasks();
            var responseDtos = taskItems.Select(t => TaskItemMapper.ToReadDto(t)).ToList();
            return Ok(responseDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadTaskItemDto>> GetById(int id)
        {
            var taskItem = await _taskService.GetTaskById(id);

            if (taskItem != null) {
                var responseDto = TaskItemMapper.ToReadDto(taskItem);
                return Ok(responseDto);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ReadTaskItemDto>> Create(CreateTaskItemDto createDto)
        {
            var taskItem = TaskItemMapper.FromCreateDto(createDto);
            var createdTaskItem = await _taskService.CreateTask(taskItem);
            var responseDto = TaskItemMapper.ToReadDto(createdTaskItem);
            //return Ok(responseDto);
            return CreatedAtAction( nameof(GetById),
                                    new { id = responseDto.Id },
                                    responseDto);
        }

        [HttpPost("{id}/complete")]
        public async Task<ActionResult<ReadTaskItemDto>> CompleteTask(int id)
        {
            var taskItem = await _taskService.CompleteTask(id);
            if (taskItem == null)
                return NotFound();

            var responseDto = TaskItemMapper.ToReadDto(taskItem);
            return Ok(responseDto);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ReadTaskItemDto>> Update(int id, UpdateTaskItemDto updateDto)
        {
            var taskItem = TaskItemMapper.FromUpdateDto(id, updateDto);
            var updatedTaskItem = await _taskService.UpdateTask(taskItem);

            if (updatedTaskItem != null) {
                var responseDto = TaskItemMapper.ToReadDto(updatedTaskItem);
                return Ok(responseDto);
            }
            return NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedTaskItem = await _taskService.DeleteTask(id);

            if (deletedTaskItem != null) {
                return NoContent();
            }
            return NotFound();
        }
    }
}
