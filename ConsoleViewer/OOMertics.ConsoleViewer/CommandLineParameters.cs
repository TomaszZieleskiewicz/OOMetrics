using CommandLine;
using OOMetrics.Abstractions.Interfaces;

namespace OOMertics.ConsoleViewer
{
    public class CommandLineParameters: IMetricsCalculatorOptions
    {
        [Option('c', "command", Required = false, HelpText = "Command that you can execute", Default = "analyze")]
        public string Command { get; set; } = "analyze";
        [Option('p', "path", Required = true, HelpText = "Path to the solution")]
        public string Path { get; set; } = string.Empty;
        [Option("ignoredDependencyNamespaces", Required = false, HelpText = "List of dependency namespaces to be ignored, for egzample System")]
        public string[] IgnoredDependencyNamespaces { get; set; } = Array.Empty<string>();
        [Option("ignoredIncomingDependencyNamespaces", Required = false, HelpText = "List of dependency namespaces to be ignored as incoming, for egzample Tests")]
        public string[] IgnoredIncomingDependencyNamespaces { get; set; } = Array.Empty<string>();
        [Option("excludeIncomingDependenciesFromOwnTests", Required = false, HelpText = "Switch if incoming dependencies from own test projects should be excluded. Own test projects are recognized for project named X by pattern: X.Tests")]
        public bool ExcludeIncomingDependenciesFromTests { get; set; } = true;
    }
}
