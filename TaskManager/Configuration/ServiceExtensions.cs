using TaskManager.Common.Features.Tasks.Application;
using TaskManager.Features.Tasks.Application;
using TaskManager.Features.Tasks.Persistence;

namespace TaskManager.Configuration
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskService, TaskService>();

            return services;
        }
    }
}
