using DealWith.ApiService.Features.VerifyItemFeature;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddTransient<VerifyItemService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapPost("/verify-item", VerifyItemEndpoints.VerifyItem);

app.MapDefaultEndpoints();

app.Run();
