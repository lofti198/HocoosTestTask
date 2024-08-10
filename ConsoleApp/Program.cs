using ConsoleApp.Tasks;

var task1 = new ThreadBackendTask();
await task1.RunAsync();

var task2 = new ExpressionBackendTask();
await task2.RunAsync();