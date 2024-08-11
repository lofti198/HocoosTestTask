namespace ConsoleApp.Shared.Tasks
{
    public class LimitedThreadBackendTask : ThreadBackendTask
    {
        private readonly SemaphoreSlim _semaphore;

        public LimitedThreadBackendTask(int maxParallelism)
        {
            // Initialize the semaphore with the provided max parallelism
            _semaphore = new SemaphoreSlim(maxParallelism); 
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

    }
}
