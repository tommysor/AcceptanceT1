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
var centralSchema = "https";
if (builder.Environment.IsDevelopment())
{
    centralSchema = "http";
}
builder.Services.AddHttpClient<ICentral, Central>(client => client.BaseAddress = new($"{centralSchema}://central"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapPost("/verify-item", VerifyItemEndpoints.VerifyItem);

app.MapDefaultEndpoints();

app.Run();
