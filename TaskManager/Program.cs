using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Configuration;
using TaskManager.Data;
using TaskManager.Middleware;
using TaskManager.Models;
using TaskManager.Repositories;
using TaskManager.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();

//Add database
builder.Services.AddDatabase();

//Configure controllers
builder.Services.AddApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.InitializeDatabase();

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

//static void InitializeDatabase(WebApplication app)
//{
//    using var scope = app.Services.CreateScope();
//    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

//    if (!context.Tasks.Any()) {
//        context.Tasks.AddRange(
//            new TaskItem { Title = "Learn ASP.NET Core", IsCompleted = false },
//            new TaskItem { Title = "Learn Controllers", IsCompleted = true }
//        );

//        context.SaveChanges();
//    }
//}