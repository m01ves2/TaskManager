using Microsoft.AspNetCore.Http;
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
        private readonly ITaskService _service;
        private readonly ILoggerService _logger;

        public TaskController(ITaskService service, ILoggerService logger)
        {
            _service = service;
            _logger = logger;
            Console.WriteLine(_service.GetId());
            Console.WriteLine(_logger.GetId());
        }

        [HttpGet]
        public ActionResult<List<ReadTaskDto>> GetAll()
        {
            var tasks = _service.GetAllTasks();
            var tasksDtos = tasks.Select(t => new ReadTaskDto() { Id = t.Id, Title = t.Title, IsCompleted = t.IsCompleted}).ToList();
            return Ok(tasksDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<ReadTaskDto> GetById(int id)
        {
            //return _items.Where(i => i.Id == id).ToList();
            var task = _service.GetTaskById(id);

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
        public ActionResult<ReadTaskDto> Create(CreateTaskDto itemDto)
        {
            Console.WriteLine("TaskController: Create");
            var item = new TaskItem() { Title =  itemDto.Title, IsCompleted = itemDto.IsCompleted }; 
            var itemCreated = _service.CreateTask(item);
            var itemCreatedDto = new ReadTaskDto() { Title = itemCreated.Title, Id = itemCreated.Id, IsCompleted = itemCreated.IsCompleted };
            return Ok(itemCreatedDto);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var itemDeleted = _service.DeleteTask(id);

            if (itemDeleted != null) {
                return NoContent();
            }
            
            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult<ReadTaskDto> Update(int id, UpdateTaskDto changedItemDto)
        {
            Console.WriteLine("TaskController: Update");
            var changedItem = new TaskItem
            {
                Id = id,
                Title = changedItemDto.Title,
                IsCompleted = changedItemDto.IsCompleted
            };

            var updatedTask = _service.UpdateTask(changedItem);

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
    }
}
