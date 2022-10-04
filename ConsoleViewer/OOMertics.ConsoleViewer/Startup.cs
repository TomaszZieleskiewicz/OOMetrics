using Microsoft.Extensions.DependencyInjection;
using OOMertics.Abstractions.Interfaces;
using OOMertics.ConsoleViewer.Commands;
using OOMertics.ConsoleViewer.Extensions;
using OOMertics.Helper.Implementations;
using OOMetrics.Abstractions.Interfaces;
using OOMetrics.Metrics;

namespace OOMertics.ConsoleViewer
{
    public static class Startup
    {
        public static ServiceCollection ConfigureServices(string[] args)
        {
            var services = new ServiceCollection();
            services.AddCommandLineOptions(args);
            services.AddMappedOptions<SolutionDeclarationProviderOptions>();
            services.AddMappedOptions<MetricsCalculatorOptions>();
            services.AddSingleton<ICommandLineWrapper, CommandLineWrapper>();
            services.AddSingleton<AnalyzeSolution>();
            services.AddSingleton<IRunner, Runner>();
            services.AddSingleton<MetricsCalculator>();
            services.AddSingleton<IDeclarationProvider, SolutionDeclarationProvider>();
            return services;
        }
    }
}
