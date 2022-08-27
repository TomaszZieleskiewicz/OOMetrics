using OOMetrics.Abstractions.Enums;

namespace OOMetrics.Abstractions.Interfaces
{
    public interface IDeclaration : IComparableByStringHash
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
