namespace OOMetrics.Abstractions.Interfaces
{
    public interface IMetricsCalculatorOptions
    {
        IEnumerable<string> IgnoredDependencyNamespaces { get; }
        bool ExcludeIncomingDependenciesFromTests { get; }
    }
}
