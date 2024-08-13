using ConsoleApp.Shared.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConsoleApp.Shared.Tasks
{
    public class LimitedThreadBackendTask : ThreadBackendTask
    {
        private readonly SemaphoreSlim _semaphore;
        private readonly ILogger<LimitedThreadBackendTask> _logger;

        public LimitedThreadBackendTask(IOptions<LimitedThreadBackendTaskOptions> options, ILogger<LimitedThreadBackendTask> logger)
        {
            _semaphore = new SemaphoreSlim(options.Value.MaxParallelism);
            _logger = logger;
        }

        protected override async Task<IEnumerable<ThreadTaskItemResult>> ExecuteAsync(IEnumerable<ThreadTaskItemConfig> configs)
        {
            var tasks = configs.Select(async config =>
            {
                await _semaphore.WaitAsync();

                try
                {
                    return await ExecuteAsync(config);
                }
                finally
                {
                    _semaphore.Release();
                }
            });

            return await Task.WhenAll(tasks);
        }

        protected override void WriteResult(ThreadTaskItemResult result)
        {
            _logger.LogInformation(result.Message);
        }
    }
}
