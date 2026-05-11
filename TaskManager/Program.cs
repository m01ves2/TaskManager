using TaskManager.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITaskService, TaskService>();

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

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

//Middleware
app.Use(async (context, next) =>
{
    Console.WriteLine("=== REQUEST START ===");

    Console.WriteLine($"Path: {context.Request.Path}");
    Console.WriteLine($"QueryString: {context.Request.QueryString}");

    await next(context);

    Console.WriteLine("=== REQUEST END ===");
});

app.Run();
