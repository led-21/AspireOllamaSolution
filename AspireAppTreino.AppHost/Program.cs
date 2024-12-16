
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

var phi35 = ollama.AddModel("phi35", "phi3.5");

builder.AddProject<Projects.BlazorChat>("blazorchat")
    .WithReference(phi35)
    .WaitFor(phi35);

builder.Build().Run();
