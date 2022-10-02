using Microsoft.Extensions.Options;
using OOMertics.Abstractions.Interfaces;
using OOMetrics.Metrics;

namespace OOMertics.ConsoleViewer.Commands
{
    public class AnalyzeSolution : ICommand
    {
        private ICommandLineWrapper _console;
        private CommandLineParameters _options;
        private MetricsCalculator _metricsCalculator;

        public AnalyzeSolution(ICommandLineWrapper console, IOptions<CommandLineParameters> options, MetricsCalculator metricsCalculator)
        {
            _console = console;
            _options = options.Value;
            _metricsCalculator = metricsCalculator;
        }
        public async Task ExecuteAsync()
        {
            _console.WriteLine($"Searching for .sln files in {_options.Path}");
            await _metricsCalculator.AnalyzeData();
            foreach (var package in _metricsCalculator.Packages)
            {
                _console.WriteLine(package.DistanceFromMainSequence.ToString());
            }
        }
    }
}
