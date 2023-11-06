using DynamicProxyDemo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTransient<LoggingInterceptor>();

builder.Services.AddService<ITaskService, TaskService>(ServiceLifetime.Transient, new Type[] { typeof(LoggingInterceptor) });

using IHost host = builder.Build();

RunTask(host.Services);

await host.RunAsync();

static void RunTask(IServiceProvider hostProvider)
{
    using IServiceScope scope = hostProvider.CreateScope();
    var provider = scope.ServiceProvider;
    var task = provider.GetRequiredService<ITaskService>();
    task.Run();
}