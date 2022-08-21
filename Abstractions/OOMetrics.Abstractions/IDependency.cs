namespace OOMetrics.Abstractions
{
    public interface IDependency
    {
        string Name { get; }
        string DependencyNamespace { get; }
        string ContainingPackage { get; }
    }
}
