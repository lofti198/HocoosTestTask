using ConsoleApp.Shared.Models;
using Microsoft.Extensions.Logging;

namespace ConsoleApp.Shared.Tasks
{
    public class LimitedDataExpressionBackendTask : ExpressionBackendTask
    {
        private readonly ILogger<LimitedDataExpressionBackendTask> _logger;

        public LimitedDataExpressionBackendTask( ILogger<LimitedDataExpressionBackendTask> logger)
        {
            _logger = logger;
        }

        protected override void WriteRecord<T>(T record, params string[] fields)
        {
            _logger.LogInformation($"Record {record.Id}:");

            var fieldMessages = fields.Select(field =>
            {
                var property = typeof(T).GetProperty(field);
                if (property != null)
                {
                    var value = property.GetValue(record);
                    return $"{field} = {value}";
                }
                else
                {
                    return $"{field} = [Property not found]";
                }
            });

            _logger.LogInformation(string.Join("; ", fieldMessages));
        }

        protected override async Task<IQueryable<T>> LoadRecords<T>(params string[] fields) 
        {
            // Simulate asynchronous operation
            await Task.Delay(100 * (fields.Length + 1));

            var originalRecords = LoadRecords<T>();

            foreach (var record in originalRecords)
            {
                var properties = typeof(T).GetProperties();

                foreach (var property in properties)
                {
                    if (!fields.Contains(property.Name))
                    {
                        // Set property to null
                        property.SetValue(record, null);
                    }
                }
            }

            return originalRecords.AsQueryable();
        }

    }

}
