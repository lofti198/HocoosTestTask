using Microsoft.Extensions.DependencyInjection;
using ConsoleApp.Shared.Tasks;
using Microsoft.Extensions.Logging;

namespace ConsoleApp.Shared.Configuration
{
    public static class ServiceConfiguration
    {
        public static ServiceProvider ConfigureAndBuildServiceProvider(int maxParallelism)
        {
            var services = new ServiceCollection();

            // Add logging service
            services.AddLogging(configure => configure.AddConsole());

            // Configure services
            ConfigureServices(services, maxParallelism);

            // Build and return the service provider
            return services.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services, int maxParallelism)
        {
            // Register tasks with the appropriate constructor parameters
            services.AddTransient<LimitedThreadBackendTask>(provider => 
                new LimitedThreadBackendTask(
                    maxParallelism,
                    provider.GetRequiredService<ILogger<LimitedThreadBackendTask>>()));
            services.AddTransient<ExpressionBackendTask>();
        }
    }
}
