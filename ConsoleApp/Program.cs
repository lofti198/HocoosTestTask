using ConsoleApp.Shared.Configuration;
using ConsoleApp.Shared.Tasks;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = ServiceConfiguration.ConfigureAndBuildServiceProvider(5);

var task1 = serviceProvider.GetRequiredService<LimitedThreadBackendTask>();
await task1.RunAsync();

var task2 = serviceProvider.GetRequiredService<ExpressionBackendTask>();
await task2.RunAsync();
