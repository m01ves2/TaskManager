using TaskManager.Configuration;
using TaskManager.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddApplicationServices();// Add services to the container.
builder.Services.AddDatabase();//Add database
builder.Services.AddApi();//Configure controllers

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

//DataBase init
app.InitializeDatabase();

//Middleware
app.UseMiddleware<ExceptionCatcher>();
//app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();
app.UseMiddleware<RequestLoggingMiddleware>();
//app.UseWelcomePage();   // WelcomePageMiddleware

// Endpoints
app.MapControllers();
app.Run();