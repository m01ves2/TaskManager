var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

//app.Use(async (context, next) =>
//{
//    Console.WriteLine("Request started");

//    await next();

//    Console.WriteLine("Request finished");
//});

//app.Use(async (HttpContext context, RequestDelegate next) =>
//{
//    Console.WriteLine("Middleware 1 BEFORE");

//    await next(context);

//    Console.WriteLine("Middleware 1 AFTER");
//});

//app.Use(async (HttpContext context, RequestDelegate next) =>
//{
//    Console.WriteLine("Middleware 2 BEFORE");

//    await next(context);

//    Console.WriteLine("Middleware 2 AFTER");
//});

//app.Run(async context =>
//{
//    Console.WriteLine("Endpoint");

//    await context.Response.WriteAsync("Hello");
//});

app.Use(async (context, next) =>
{
    Console.WriteLine("=== REQUEST START ===");

    Console.WriteLine($"Path: {context.Request.Path}");
    Console.WriteLine($"QueryString: {context.Request.QueryString}");

    await next(context);

    Console.WriteLine("=== REQUEST END ===");
});

app.Run();
