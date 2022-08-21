namespace OOMetrics.Metrics
{
    public class MetricsCalculatorOptions
    {
        public string[] IgnoredDependencyNameSpaces { get; init; } = Array.Empty<string>();
        public bool ExcludeIncomingDependenciesFromTests { get; init; } = true;
    }
}
