namespace OOMetrics.Metrics.Interfaces
{
    public interface IDependency
    {
        string Name { get; }
        string DependencyNamespace { get; }
        string ContainingAssembly { get; }
    }
}
