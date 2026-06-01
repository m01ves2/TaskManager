using Microsoft.EntityFrameworkCore;
using TaskManager.Features.Tasks.Domain;

namespace TaskManager.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<TaskItem> Tasks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
