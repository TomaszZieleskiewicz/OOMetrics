using CommandLine;
using OOMetrics.Abstractions.Interfaces;

namespace OOMertics.ConsoleViewer
{
    public class CommandLineParameters: IMetricsCalculatorOptions, ISolutionDeclarationProviderOptions
    {
        [Option('c', "command", Required = false, HelpText = "Command that you can execute", Default = "analyze")]
        public string Command { get; init; } = "analyze";
        [Option('p', "path", Required = true, HelpText = "Path to the solution")]
        public string Path { get; init; } = string.Empty;
        [Option('s', "solution", Required = true, HelpText = "Name of the solution")]
        public string SolutionName { get; init; } = string.Empty;
        [Option('n', "namespacesToAnalyze", Required = false, HelpText = "List of namespaces to analyze. Rest will be ignored, default is everything")]
        public IEnumerable<string> NamespacesToAnalyze { get; init; } = new List<string>() { };
        [Option("excludeIncomingDependenciesFromOwnTests", Required = false, HelpText = "Switch if incoming dependencies from own test projects should be excluded. Own test projects are recognized for project named X by pattern: X.Tests")]
        public bool ExcludeIncomingDependenciesFromTests { get; init; } = true;
    }
}
