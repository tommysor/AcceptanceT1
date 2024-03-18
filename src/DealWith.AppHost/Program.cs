var builder = DistributedApplication.CreateBuilder(args);

var centralTestDouble = builder.AddProject<Projects.CentralTestDouble>("central");

var apiService = builder.AddProject<Projects.DealWith_ApiService>("apiservice")
    .WithReference(centralTestDouble);

builder.AddProject<Projects.DealWith_Web>("webfrontend")
    .WithReference(apiService);

builder.Build().Run();
