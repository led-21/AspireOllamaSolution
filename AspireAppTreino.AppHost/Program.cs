
var builder = DistributedApplication.CreateBuilder(args);

//var cosmos = builder.AddAzureCosmosDB("cosmos")
//    .RunAsEmulator(opt =>
//{
//    opt.WithLifetime(ContainerLifetime.Persistent);
//    opt.WithDataVolume();
//});

//var cosmosdb = cosmos.AddDatabase("cosmosdb");

var ollama = builder.AddOllama("ollama")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithOtlpExporter();

var phi3 = ollama.AddModel("phi3", "phi3");

builder.AddProject<Projects.BlazorChat>("blazorchat")
    .WithReference(phi3)
    .WaitFor(phi3);

builder.Build().Run();
