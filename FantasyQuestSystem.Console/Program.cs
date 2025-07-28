using FantasyQuestSystem.Infrastructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FantasyQuestSystem.Console;

class Program
{
    static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var consoleApp = host.Services.GetRequiredService<ConsoleApplication>();
        await consoleApp.RunAsync();
    }

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddFantasyQuestSystem();
                services.AddSingleton<ConsoleApplication>();
            });
}
