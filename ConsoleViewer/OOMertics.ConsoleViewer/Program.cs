using Microsoft.Extensions.DependencyInjection;
using OOMertics.ConsoleViewer.Commands;
using OOMertics.ConsoleViewer.Extensions;
using OOMertics.Abstractions.Interfaces;

namespace OOMertics.ConsoleViewer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            services
                ?.AddCommandLineOptions(args)
                ?.AddSingleton<IRunner, Runner>()
                ?.BuildServiceProvider()
                ?.GetService<IRunner>()
                ?.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICommandLineWrapper, CommandLineWrapper>();
            services.AddSingleton<AnalyzeSolution>();
        }
    }
}