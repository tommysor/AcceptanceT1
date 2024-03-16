var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.DealWith_ApiService>("apiservice");

builder.AddProject<Projects.DealWith_Web>("webfrontend")
    .WithReference(apiService);

builder.Build().Run();
