using Microsoft.Extensions.DependencyInjection;
using ConsoleApp.Shared.Tasks;

namespace ConsoleApp.Shared.Configuration
{
    public static class ServiceConfiguration
    {
        public static ServiceProvider ConfigureAndBuildServiceProvider(int maxParallelism)
        {
            var services = new ServiceCollection();

            // Configure services
            ConfigureServices(services, maxParallelism);

            // Build and return the service provider
            return services.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services, int maxParallelism)
        {
            // Register tasks with the appropriate constructor parameters
            services.AddTransient<LimitedThreadBackendTask>(provider => new LimitedThreadBackendTask(maxParallelism));
            services.AddTransient<ExpressionBackendTask>();
        }
    }
}
