namespace OOMetrics.Abstractions
{
    public interface IDeclaration
    {
        string Name { get; }
        DeclarationType Type { get; }
        string DeclarationNamespace { get; }
        string ContainingPackage { get; }
        bool IsAbstract { get; }
        ICollection<IDependency> Dependencies { get; }
        IDependency ToDependency();
    }
}
