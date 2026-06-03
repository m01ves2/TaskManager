using Microsoft.EntityFrameworkCore;
using TaskManager.Features.Tasks.Domain;
using TaskManager.Infrastructure;

namespace TaskManager.Configuration
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite("Data Source=tasks.db");
            });

            return services;
        }

        public static void InitializeDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (!context.Tasks.Any()) {
                context.Tasks.AddRange(
                    new TaskItem { Title = "Learn ASP.NET Core", Status = TaskItemStatus.New },
                    new TaskItem { Title = "Learn Controllers", Status = TaskItemStatus.InProgress }
                );

                context.SaveChanges();
            }
        }
    }
}
