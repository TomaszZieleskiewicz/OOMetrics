using Microsoft.Extensions.DependencyInjection;
using OOMertics.ConsoleViewer.Commands;
using OOMertics.ConsoleViewer.Extensions;
using OOMertics.ConsoleViewer.Interfaces;

namespace OOMertics.ConsoleViewer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            services
                ?.AddCommandLineOptions()
                ?.AddSingleton<IRunner, Runner>()
                ?.BuildServiceProvider()
                ?.GetService<IRunner>()
                ?.Run(args);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICommandLineWrapper, CommandLineWrapper>();
            services.AddSingleton<AnalyzeSolution>();
        }
    }
}