using TestDoubles.CentralTestDouble.Features.CentralVerifyFeature;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
var app = builder.Build();

app.MapGet("/", () => "Test double for the central service.");
app.MapPost("/central-verify", CentralVerifyEndpoints.VerifyItem);
app.MapPost("/central-verify-set-next-response", CentralVerifyEndpoints.VerifyItemNextResponse);

app.MapDefaultEndpoints();

app.Run();
