namespace OOMetrics.Abstractions.Interfaces
{
    public interface IMetricsCalculatorOptions
    {
        string[] IgnoredDependencyNamespaces { get; }
        string[] IgnoredIncomingDependencyNamespaces { get; }
        bool ExcludeIncomingDependenciesFromTests { get; }
    }
}
