using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Middleware;
using TaskManager.Models;
using TaskManager.Rspositories;
using TaskManager.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source=tasks.db");
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

SeedDatabase(app);


//app.UseHttpsRedirection();

//app.UseAuthorization();


//Middleware
app.UseMiddleware<ExceptionCatcher>();

app.Use(async (context, next) =>
{
    Console.WriteLine("=== REQUEST START ===");

    Console.WriteLine($"Path: {context.Request.Path}");
    Console.WriteLine($"QueryString: {context.Request.QueryString}");

    await next(context);

    Console.WriteLine("=== REQUEST END ===");
});


//app.UseWelcomePage();   // подключение WelcomePageMiddleware

app.MapControllers();
app.Run();

static void SeedDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Tasks.Any()) {
        context.Tasks.AddRange(
            new TaskItem { Title = "Learn ASP.NET Core", IsCompleted = false },
            new TaskItem { Title = "Learn Controllers", IsCompleted = true }
        );

        context.SaveChanges();
    }
}