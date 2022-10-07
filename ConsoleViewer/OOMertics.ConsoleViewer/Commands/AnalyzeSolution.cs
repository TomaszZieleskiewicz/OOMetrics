using Microsoft.Extensions.Options;
using OOMertics.Abstractions.Interfaces;
using OOMertics.Helper.Implementations;
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
            _console.WritingEnabled = _options.OutputVerbosity > 1;
            _console.WriteLine($"Analyzing {(_options.PackagesToAnalyze.Count() > 0 ? string.Join(',', _options.PackagesToAnalyze) : "everything")} in {_options.Path}{_options.SolutionName}.sln ...");
            await _metricsCalculator.AnalyzeData();

            _console.WritingEnabled = _options.OutputVerbosity > 0;
            _console.WriteLine($"Solution analyzed, {_metricsCalculator.Packages.Count} packages found.");
            _console.WriteLine($"Total distance: {_metricsCalculator.Packages.Select(p => p.DistanceFromMainSequence).Sum():N2}");
            _console.WriteLine($"Average distance: {_metricsCalculator.Packages.Select(p => p.DistanceFromMainSequence).Average():N2}");
            _console.WriteLine(string.Empty);
            _console.WriteLine(string.Empty);

            _console.WritingEnabled = _options.OutputVerbosity > 1;
            foreach (var package in _metricsCalculator.Packages)
            {
                _console.WriteLine($"{package.Name}", 1, GetColor(package.DistanceFromMainSequence));
                _console.WriteLine($"Distance: {package.DistanceFromMainSequence:N2} = |{package.Instability:N2} + {package.Abstractness:N2} - 1|", 2);
                if(package.DistanceFromMainSequence > 0)
                {
                    if(package.Instability != 0 && package.Instability != 1)
                    {
                        _console.WriteLine($"Instability: {package.Instability} = {package.EfferenCoupling} / ({package.EfferenCoupling} + {package.AfferenCoupling})",3);
                        _console.WriteLine($"Outgoing dependencies (efferent coupling): {package.EfferenCoupling}", 3);
                        foreach (var outgoing in package.OutgoingDependencies)
                        {
                            _console.WriteLine($"- {outgoing}", 4);
                        }
                        _console.WriteLine($"Incoming dependencies (afferent coupling): {package.AfferenCoupling}", 3);
                        foreach (var incoming in package.IncomingDependencies)
                        {
                            _console.WriteLine($"- {incoming}", 4);
                        }
                        _console.WriteLine(string.Empty);
                    }
                    if (package.Abstractness != 0 && package.Abstractness != 1)
                    {
                        var abstractDeclarations = package.GetAbstractDeclarations();
                        _console.WriteLine($"Abstractness: {package.Abstractness} = {abstractDeclarations.Count()} / {package.Declarations.Count()}", 3);
                        foreach (var declaration in package.Declarations)
                        {
                            _console.WriteLine($"- {declaration}", 4);
                        }
                    }
                }
                _console.WriteLine(string.Empty);
                _console.WriteLine(string.Empty);
            }
        }
        private ConsoleColor GetColor(decimal distance)
        {
            return distance switch
            {
                0 => ConsoleColor.Green,
                < 0.33M => ConsoleColor.Yellow,
                < 0.66M => ConsoleColor.DarkYellow,
                _ => ConsoleColor.Red,
            };
        }
    }
}
