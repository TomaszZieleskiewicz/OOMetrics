using Microsoft.Extensions.DependencyInjection;
using OOMertics.Abstractions.Interfaces;
using OOMertics.ConsoleViewer.Commands;
using OOMertics.ConsoleViewer.Extensions;

namespace OOMertics.ConsoleViewer
{
    public static class Startup
    {
        public static ServiceCollection ConfigureServices(string[] args)
        {
            var services = new ServiceCollection();
            services.AddCommandLineOptions(args);
            services.AddSingleton<ICommandLineWrapper, CommandLineWrapper>();
            services.AddSingleton<AnalyzeSolution>();
            services.AddSingleton<IRunner, Runner>();           
            
            return services;
        }
    }
}
