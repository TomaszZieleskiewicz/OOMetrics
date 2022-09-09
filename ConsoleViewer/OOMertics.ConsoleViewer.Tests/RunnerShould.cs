using Microsoft.Extensions.DependencyInjection;
using OOMertics.Abstractions.Interfaces;
using OOMertics.ConsoleViewer.Commands;
using OOMertics.ConsoleViewer.Extensions;
using System.Diagnostics;

namespace OOMertics.ConsoleViewer.Tests
{
    public class RunnerShould
    {
        [Fact]
        public void Test1()
        {
            var goodParams = new List<string> { "-p", "E:\\Poligon\\github\\OOMetrics" };
            var services = new ServiceCollection();
            services.AddSingleton<ICommandLineWrapper, CommandLineWrapper>();
            services.AddSingleton<AnalyzeSolution>();
            services
                ?.AddCommandLineOptions(goodParams.ToArray())
                ?.AddSingleton<IRunner, Runner>()
                ?.BuildServiceProvider()
                ?.GetService<IRunner>()
                ?.Run();
        }
    }
}