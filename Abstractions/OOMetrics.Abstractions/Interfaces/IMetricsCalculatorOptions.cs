namespace OOMetrics.Abstractions.Interfaces
{
    public interface IMetricsCalculatorOptions
    {
        IEnumerable<string> IgnoredDependencyNamespaces { get; init; }
        bool ExcludeIncomingDependenciesFromTests { get; init; }
    }
}
