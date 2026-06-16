using System.Text.Json;
using System.Text.Json.Serialization;
using TaskManager.UI.Components;
using TaskManager.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<TaskApiService>();

builder.Services.AddScoped(sp =>
    new HttpClient(new HttpClientHandler())
    {
        BaseAddress = new Uri("https://localhost:7191"),
        DefaultRequestHeaders =
        {
            Accept = { new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json") }
        }
    }
);

// Оборачиваем HttpClient в JsonSerializerOptions с конвертером
builder.Services.AddSingleton(new JsonSerializerOptions
{
    Converters = { new JsonStringEnumConverter() },
    PropertyNameCaseInsensitive = true
});

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
