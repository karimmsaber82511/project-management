var builder = DistributedApplication.CreateBuilder(args);

// Add the .NET API project (strongly-typed from ProjectReference)
var api = builder.AddProject<Projects.CompanyEmployeeProject_HttpApi_Host>("api");

// Add the Angular frontend; use absolute path so frontend-installer (npm install) runs in the correct directory
var appHostOutputDir = AppContext.BaseDirectory;
// From bin/Debug/net10.0 go up to repo root (6 levels: net10.0 -> Debug -> bin -> AppHost -> src -> aspnet-core -> repo root)
var repoRoot = Path.GetFullPath(Path.Combine(appHostOutputDir, "..", "..", "..", "..", "..", ".."));
var frontendPath = Path.Combine(repoRoot, "angular");
builder.AddJavaScriptApp("frontend", frontendPath, "start:aspire")
    .WithNpm(installArgs: ["--legacy-peer-deps"])
    .WaitFor(api)
    .WithReference(api)
    .WithHttpEndpoint(port: 4242, env: "PORT")
    .WithExternalHttpEndpoints();

await builder.Build().RunAsync();
