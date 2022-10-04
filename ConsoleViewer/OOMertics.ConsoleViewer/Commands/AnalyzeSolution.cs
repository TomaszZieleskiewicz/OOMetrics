using Microsoft.Extensions.Options;
using OOMertics.Abstractions.Interfaces;
using OOMetrics.Metrics;
using System.Text;

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
            _console.WriteLine($"Analyzing {_options.Path}...");
            await _metricsCalculator.AnalyzeData();
            _console.WriteLine($"Solution analyzed, {_metricsCalculator.Packages.Count} packages found.");
            foreach (var package in _metricsCalculator.Packages)
            {
                var color = package.DistanceFromMainSequence switch
                {
                    0 => ConsoleColor.Green,
                    < 0.33M => ConsoleColor.Yellow,
                    < 0.66M => ConsoleColor.DarkYellow,
                    _ => ConsoleColor.Red,
                };
                _console.WriteLine($"\t{package.Name}",color);
                _console.WriteLine($"\t\tDistance: {package.DistanceFromMainSequence:N2} = |{package.Instability:N2} + {package.Abstractness:N2} - 1|");
                if(package.DistanceFromMainSequence > 0)
                {
                    if(package.Instability != 0 && package.Instability != 1)
                    {
                        _console.WriteLine($"\t\t\tInstability: {package.Instability} = {package.EfferenCoupling} / ({package.EfferenCoupling} + {package.AfferenCoupling})");
                        _console.WriteLine($"\t\t\tOutgoing dependencies (efferent coupling): {package.EfferenCoupling}");
                        foreach (var outgoing in package.OutgoingDependencies)
                        {
                            _console.WriteLine($"\t\t\t\t- {outgoing}");
                        }
                        _console.WriteLine($"\t\t\tIncoming dependencies (afferent coupling): {package.AfferenCoupling}");
                        foreach (var incoming in package.IncomingDependencies)
                        {
                            _console.WriteLine($"\t\t\t\t- {incoming}");
                        }
                        _console.WriteLine(string.Empty);
                    }
                    if (package.Abstractness != 0 && package.Abstractness != 1)
                    {
                        var abstractDeclarations = package.GetAbstractDeclarations();
                        _console.WriteLine($"\t\t\tAbstractness: {package.Abstractness} = {abstractDeclarations.Count()} / ({abstractDeclarations.Count()} + {package.Declarations.Count()})");
                        foreach (var declaration in package.Declarations)
                        {
                            _console.WriteLine($"\t\t\t\t- {declaration}");
                        }
                    }
                }
                _console.WriteLine(string.Empty);
                _console.WriteLine(string.Empty);
            }
        }
    }
}
