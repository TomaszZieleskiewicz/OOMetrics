namespace OOMetrics.Abstractions.Interfaces
{
    public class MetricsCalculatorOptions: IMetricsCalculatorOptions
    {
        public IEnumerable<string> IgnoredDependencyNamespaces { get; init; } = new List<string>();
        public bool ExcludeIncomingDependenciesFromTests { get; init; } = true;
    }
}
