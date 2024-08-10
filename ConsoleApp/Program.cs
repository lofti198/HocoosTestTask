using ConsoleApp.Shared.Configuration;
using ConsoleApp.Shared.Tasks;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = ServiceConfiguration.ConfigureAndBuildServiceProvider();

var task1 = serviceProvider.GetRequiredService<ThreadBackendTask>();
await task1.RunAsync();

var task2 = serviceProvider.GetRequiredService<ExpressionBackendTask>();
await task2.RunAsync();
