namespace OOMetrics.Abstractions.Interfaces
{
    public interface IMetricsCalculatorOptions
    {
        IEnumerable<string> NamespacesToAnalyze { get; init; }
        bool ExcludeIncomingDependenciesFromTests { get; init; }
    }
}
