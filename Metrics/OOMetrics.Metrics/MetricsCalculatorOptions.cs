namespace OOMetrics.Abstractions.Interfaces
{
    public class MetricsCalculatorOptions: IMetricsCalculatorOptions
    {
        public IEnumerable<string> PackagesToAnalyze { get; init; } = new List<string>();
        public bool ExcludeIncomingDependenciesFromTests { get; init; } = true;
        public bool IncludeExtrenalDependencies { get; init; } = false;
        public string TestProjectNamePattern { get; init; } = "{0}.Tests";
    }
}
