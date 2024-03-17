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
builder.Services.AddSingleton<ICentral, Central>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapPost("/verify-item", VerifyItemEndpoints.VerifyItem);

app.MapDefaultEndpoints();

app.Run();
