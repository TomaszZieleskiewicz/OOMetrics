namespace OOMetrics.Metrics
{
    public class MetricsCalculatorOptions
    {
        public string[] IgnoredDependencyNamespaces { get; init; } = Array.Empty<string>();
        public string[] IgnoredIncomingDependencyNamespaces { get; init; } = Array.Empty<string>();
        public bool ExcludeIncomingDependenciesFromTests { get; init; } = true;
    }
}
