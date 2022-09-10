namespace OOMetrics.Abstractions.Interfaces
{
    public interface IDependency : IComparableByStringHash
    {
        string Name { get; }
        string DependencyNamespace { get; }
        string ContainingPackage { get; }
    }
}
