namespace OOMetrics.Abstractions.Interfaces
{
    public interface IMetricsCalculatorOptions
    {
        IEnumerable<string> PackagesToAnalyze { get; init;  }
        bool ExcludeIncomingDependenciesFromTests { get; init; }
        bool IncludeExtrenalDependencies { get; init; }
        string TestProjectNamePattern { get; init; }
    }
}
