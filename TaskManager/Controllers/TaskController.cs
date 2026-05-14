using Microsoft.AspNetCore.Mvc;
using TaskManager.Dtos;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILoggerService _loggerService;

        public TaskController(ITaskService service, ILoggerService loggerService)
        {
            //Console.WriteLine("TaskController Created");
            
            _taskService = service;
            _loggerService = loggerService;
            //Console.WriteLine(_taskService.GetId());
            Console.WriteLine(_loggerService.GetId());
        }

        [HttpGet]
        public async Task<ActionResult<List<ReadTaskDto>>> GetAll()
        {
            var tasks = await _taskService.GetAllTasks();
            var tasksDtos = tasks.Select(t => new ReadTaskDto() 
            { 
                Id = t.Id, 
                Title = t.Title, 
                IsCompleted = t.IsCompleted
            }).ToList();
            return Ok(tasksDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadTaskDto>> GetById(int id)
        {
            var task = await _taskService.GetTaskById(id);

            if( task != null) {
                var taskDto = new ReadTaskDto()
                {
                    Id = task.Id,
                    Title = task.Title,
                    IsCompleted = task.IsCompleted
                };
                return Ok(taskDto);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ReadTaskDto>> Create(CreateTaskDto itemDto)
        {
            var item = new TaskItem() { 
                Title =  itemDto.Title, 
                IsCompleted = itemDto.IsCompleted 
            }; 
            var itemCreated = await _taskService.CreateTask(item);
            var itemCreatedDto = new ReadTaskDto() 
            { 
                Title = itemCreated.Title, 
                Id = itemCreated.Id, 
                IsCompleted = itemCreated.IsCompleted 
            };
            return Ok(itemCreatedDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReadTaskDto>> Update(int id, UpdateTaskDto changedItemDto)
        {
            var changedItem = new TaskItem
            {
                Id = id,
                Title = changedItemDto.Title,
                IsCompleted = changedItemDto.IsCompleted
            };

            var updatedTask = await _taskService.UpdateTask(changedItem);

            if (updatedTask != null) {
                var updatedTaskDto = new ReadTaskDto
                {
                    Id = updatedTask.Id,
                    Title = updatedTask.Title,
                    IsCompleted = updatedTask.IsCompleted
                };

                return Ok(updatedTaskDto);
            }

            return NotFound();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var itemDeleted = await _taskService.DeleteTask(id);

            if (itemDeleted != null) {
                return NoContent();
            }

            return NotFound();
        }

        [HttpGet("slow")]
        public async Task<IActionResult> Slow()
        {
            Console.WriteLine($"START Slow: {DateTime.Now:HH:mm:ss.fff}");

            await Task.Delay(5000); // имитация долгой операции

            Console.WriteLine($"END Slow: {DateTime.Now:HH:mm:ss.fff}");

            return Ok("Slow finished");
        }


        [HttpGet("fast")]
        public IActionResult Fast()
        {
            Console.WriteLine($"FAST: {DateTime.Now:HH:mm:ss.fff}");
            return Ok("Fast response");
        }
    }
}
