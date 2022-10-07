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
        [Option('o', "outputVerbosity", Required = false, HelpText = "What will be logged to output: 0 - silent, 1 - only total distance, 2 - detailed report")]
        public int OutputVerbosity { get; init; } = 2;
        [Option('f', "packagesToAnalyze", Required = false, HelpText = "List of solution packages to analyze. Rest will be ignored, default is all namespaces from solution")]
        public IEnumerable<string> PackagesToAnalyze { get; init; } = new List<string>() { };
        [Option("excludeIncomingDependenciesFromOwnTests", Required = false, HelpText = "Switch if incoming dependencies from own test projects should be excluded. Own test projects are recognized testProjectNamePattern")]
        public bool ExcludeIncomingDependenciesFromTests { get; init; } = true;
        [Option("includeExtrenalDependencies", Required = false, HelpText = "Switch to controll taking dependencies from outside of solution, like System or nuggets, into consideration. Default false.")]
        public bool IncludeExtrenalDependencies { get; init; } = false;
        [Option("testProjectNamePattern", Required = false, HelpText = "Pattern to determine if Package contains test for another package. {0} will be replaced by current package name, so for package X default pattern will return X.Tests. Default {0}.Tests.")]
        public string TestProjectNamePattern { get; init; } = "{0}.Tests";
    }
}
