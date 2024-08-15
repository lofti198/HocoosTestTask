using ConsoleApp.Shared.Configuration;
using ConsoleApp.Shared.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var serviceProvider = new ServiceCollection()
    .AddLogging(configure => configure.AddConsole())
    .Configure<LimitedThreadBackendTaskOptions>(options =>
    {
        options.MaxParallelism = 5;
    })
    .AddTransient<LimitedThreadBackendTask>()
    .AddTransient<LimitedDataExpressionBackendTask>()
    .BuildServiceProvider();

var task1 = serviceProvider.GetRequiredService<LimitedThreadBackendTask>();
await task1.RunAsync();

var task2 = serviceProvider.GetRequiredService<LimitedDataExpressionBackendTask>();
await task2.RunAsync();
