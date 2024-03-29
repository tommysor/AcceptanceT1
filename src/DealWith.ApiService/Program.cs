using DealWith.ApiService.Features.VerifyItemFeature;
using DealWith.ApiService.Features.VerifyItemFeature.Infrastructure;
using DealWith.ApiService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddTransient<VerifyItemService>();
builder.Services.AddSingleton<IStorage, Storage>();
builder.Services.AddTransient<ICentral, Central>();
builder.Services.AddHttpClient<ICentral, Central>(client => client.BaseAddress = new("https://central"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapPost("/verify-item", VerifyItemEndpoints.VerifyItem);

app.MapDefaultEndpoints();

var logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger("Configuration");
var configuration = app.Services.GetRequiredService<IConfiguration>();
var configs = configuration.AsEnumerable();
foreach (var config in configs)
{
    logger.LogInformation($"{config.Key}: {config.Value}");
}

app.Run();
