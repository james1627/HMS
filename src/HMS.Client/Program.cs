using HMS.Infrastructure.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddInfrastructureServices(builder.Configuration);

await builder.Build().RunAsync();


