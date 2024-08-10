using Microsoft.Extensions.DependencyInjection;

using ConsoleApp.Shared.Tasks;

namespace ConsoleApp.Shared.Configuration
{
    public static class ServiceConfiguration
    {
        public static ServiceProvider ConfigureAndBuildServiceProvider()
        {
            var services = new ServiceCollection();

            // Configure services
            ConfigureServices(services);

            // Build and return the service provider
            return services.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Register tasks
            services.AddTransient<ThreadBackendTask>();
            services.AddTransient<ExpressionBackendTask>();
        }
    }
}
