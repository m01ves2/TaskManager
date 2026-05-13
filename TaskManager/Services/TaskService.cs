using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Rspositories;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        private ITaskRepository _repository;
        private Guid _id = Guid.NewGuid();
        public Guid GetId() => _id;

        public TaskService(ITaskRepository repository)
        {
            Console.WriteLine("TaskService created");
            _repository = repository;
        }

        public List<TaskItem> GetAllTasks() => _repository.GetAllTasks();
        public TaskItem? GetTaskById(int id) => _repository.GetTaskById(id);

        public TaskItem CreateTask(TaskItem item)
        {
            return _repository.CreateTask(item);
        }
        public TaskItem? UpdateTask(TaskItem changedItem)
        {
            return (_repository.UpdateTask(changedItem));
        }

        public TaskItem? DeleteTask(int id)
        {
            return _repository.DeleteTask(id);
        }

    }
}
